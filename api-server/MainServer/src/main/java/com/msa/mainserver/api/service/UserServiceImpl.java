package com.msa.mainserver.api.service;

import java.time.LocalDateTime;
import java.util.Optional;
import java.util.UUID;
import java.util.concurrent.TimeUnit;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.msa.mainserver.common.exception.CustomException;
import com.msa.mainserver.common.exception.CustomExceptionType;
import com.msa.mainserver.db.entity.User;
import com.msa.mainserver.db.entity.UserActivity;
import com.msa.mainserver.db.entity.UserAmount;
import com.msa.mainserver.db.entity.UserAmountLog;
import com.msa.mainserver.db.repository.UserActivityRepository;
import com.msa.mainserver.db.repository.UserAmountLogRepository;
import com.msa.mainserver.db.repository.UserAmountRepository;
import com.msa.mainserver.db.repository.UserRepository;
import com.msa.mainserver.dto.enums.CheckDuplicateType;
import com.msa.mainserver.dto.request.AmountChangeRequest;
import com.msa.mainserver.dto.request.CheckDuplicateRequest;
import com.msa.mainserver.dto.request.LoginRequest;
import com.msa.mainserver.dto.request.RegisterRequest;
import com.msa.mainserver.dto.request.WithdrawalUserRequest;
import com.msa.mainserver.dto.response.LoginResponse;
import com.msa.mainserver.util.BcryptUtil;
import com.msa.mainserver.util.EmailUtil;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@Service
@RequiredArgsConstructor
@Transactional(readOnly = true)
@Slf4j
public class UserServiceImpl implements UserService {

	private final UserRepository userRepository;
	private final UserActivityRepository userActivityRepository;
	private final UserAmountRepository userAmountRepository;
	private final UserAmountLogRepository userAmountLogRepository;
	private final BcryptUtil bcryptUtil;
	private final EmailUtil emailUtil;
	private final RedisTemplate<String, String> redisTemplate;

	@Value("${spring.mail.verify-link}")
	private String verifyLink;

	@Override
	@Transactional
	public void userRegister(RegisterRequest registerRequest) {

		String encryptedPw = bcryptUtil.encryptPassword(registerRequest.getPassword());

		User user = User.builder()
			.email(registerRequest.getEmail())
			.password(encryptedPw)
			.nickname(registerRequest.getNickname())
			.regDate(LocalDateTime.now())
			.userActive(false)
			.build();

		User saveUser = userRepository.save(user);

		UserActivity userActivity = UserActivity.builder()
			.user(saveUser)
			.recentLoginTime(null)
			.recentLoginIp(null)
			.shortestEscapeTime(null)
			.longestSurvivalTime(null)
			.normalMonsterKills(0)
			.eliteMonsterKills(0)
			.deathCount(0)
			.totalQuestCompleted(0)
			.totalItemCrafted(0)
			.totalEscapeCount(0)
			.totalPlayTime(0)
			.build();

		userActivityRepository.save(userActivity);

		UserAmount userAmount = UserAmount.builder()
			.user(saveUser)
			.userAmount(0)
			.build();
		userAmountRepository.save(userAmount);

		sendVerificationMail(saveUser.getEmail());
	}

	@Override
	public void checkDuplicateInfo(CheckDuplicateRequest request) {

		CheckDuplicateType type = request.getType();

		switch (type) {
			case EMAIL:
				Optional<User> findByEmail = userRepository.findByEmail(request.getInfo());
				if (findByEmail.isPresent())
					throw new CustomException(CustomExceptionType.DUPLICATE_EMAIL_EXCEPTION);
				break;
			case NICKNAME:
				Optional<User> findByNickname = userRepository.findByNickname(request.getInfo());
				if (findByNickname.isPresent())
					throw new CustomException(CustomExceptionType.DUPLICATE_NICKNAME_EXCEPTION);
				break;
		}

	}

	@Override
	@Transactional
	public LoginResponse userLogin(LoginRequest request, HttpServletRequest httpRequest) {

		Optional<UserActivity> userActivity = userActivityRepository.findByUserEmail(request.getEmail());

		if (!userActivity.isPresent())
			throw new CustomException(CustomExceptionType.USER_NOT_FOUND);

		User findUser = userActivity.get().getUser();

		if (!findUser.isUserActive())
			throw new CustomException(CustomExceptionType.UN_VERIFICATION_EXCEPTION);

		boolean isCanLogin = bcryptUtil.checkPassword(request.getPassword(), findUser.getPassword());

		if (!isCanLogin)
			throw new CustomException(CustomExceptionType.WRONG_PASSWORD_EXCEPTION);

		userActivity.get().setRecentLoginIp(getClientIp(httpRequest));
		userActivity.get().setRecentLoginTime(LocalDateTime.now());

		Optional<UserAmount> findUserAmount = userAmountRepository.findById(findUser.getId());

		LoginResponse response = LoginResponse.builder()
			.id(findUser.getId())
			.nickname(findUser.getNickname())
			.amount(findUserAmount.get().getUserAmount())
			.build();

		return response;
	}

	@Override
	@Transactional
	public void withdrawalUser(WithdrawalUserRequest request) {
		Optional<User> findById = userRepository.findById(request.getId());

		if (!findById.isPresent())
			throw new CustomException(CustomExceptionType.USER_NOT_FOUND);

		findById.get().withdrawalUser();
	}

	@Override
	public void sendVerificationMail(String email) {
		UUID uuid = UUID.randomUUID();
		String key = uuid.toString();
		String content = "";
		content += "<div style='margin:20px;'>";
		content += "<h1> 안녕하세요 MSA 입니다. </h1>";
		content += "<br>";
		content += "<p>인증하시려면 아래 링크를 클릭해주세요</p>";
		content += "<br>";
		content += "<p>감사합니다.</p>";
		content += "<br>";
		content += "<div align='center' style='border:1px solid black; font-family:verdana';>";
		content += "<h3 style='color:blue;'>회원가입 인증 링크입니다.</h3>";
		content += "<div style='font-size:130%'>";
		content += "인증하려면 클릭하세요 : <a href =" + verifyLink + key;
		content += " style='color:red;'>인증 링크</a><div><br/> ";
		content += "</div>";
		emailUtil.sendEmail(email, "[MSA] 회원가입 인증메일입니다.", content);
		redisTemplate.opsForValue().set(key, email, 10, TimeUnit.MINUTES);
	}

	@Override
	@Transactional
	public String getVerifyEmail(String uuid) {
		log.info("" + uuid);
		String key = uuid;
		String email = redisTemplate.opsForValue().get(key);
		if (email == null) {
			return "<script>alert('인증 기간이 만료되었습니다') window.close();</script>";
		} else {
			Optional<User> findByEmail = userRepository.findByEmail(email);
			findByEmail.get().setUserActive(true);
			return "<script>alert('메일 인증이 완료되었습니다'); window.close();</script>";
		}
	}

	@Override
	@Transactional
	public int changeUserAmount(AmountChangeRequest request) {

		Optional<UserAmount> findUserAmount = userAmountRepository.findById(request.getUserId());
		if (!findUserAmount.isPresent())
			throw new CustomException(CustomExceptionType.USER_NOT_FOUND);

		findUserAmount.get().setUserAmount(findUserAmount.get().getUserAmount() + request.getAmount());

		UserAmountLog userAmountLog = UserAmountLog.builder()
			.amountChange(request.getAmount())
			.user(findUserAmount.get().getUser())
			.totalAmount(findUserAmount.get().getUserAmount())
			.build();

		userAmountLogRepository.save(userAmountLog);

		return request.getAmount();
	}

	/**
	 * 접속한 클라이언트의 IP를 알아내기 위한 메소드
	 * @param request
	 * @return
	 */
	private String getClientIp(HttpServletRequest request) {
		String remoteAddr = request.getRemoteAddr();
		String forwarded = request.getHeader("X-Forwarded-For");
		String realIp = request.getHeader("X-Real-IP");

		if (forwarded == null) {
			return realIp == null ? remoteAddr : realIp;
		} else {
			return realIp == null ? forwarded.split(",")[0] : realIp;
		}
	}

}
