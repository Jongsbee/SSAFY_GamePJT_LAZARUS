package com.msa.mainserver.api.service;

import java.time.LocalDateTime;
import java.util.Optional;

import javax.servlet.http.HttpServletRequest;

import org.slf4j.Logger;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.msa.mainserver.common.exception.CustomException;
import com.msa.mainserver.common.exception.CustomExceptionType;
import com.msa.mainserver.db.entity.User;
import com.msa.mainserver.db.entity.UserActivity;
import com.msa.mainserver.db.entity.UserAmount;
import com.msa.mainserver.db.repository.UserActivityRepository;
import com.msa.mainserver.db.repository.UserAmountRepository;
import com.msa.mainserver.db.repository.UserRepository;
import com.msa.mainserver.dto.enums.CheckDuplicateType;
import com.msa.mainserver.dto.request.CheckDuplicateRequest;
import com.msa.mainserver.dto.request.LoginRequest;
import com.msa.mainserver.dto.request.RegisterRequest;
import com.msa.mainserver.dto.request.WithdrawalUserRequest;
import com.msa.mainserver.dto.response.LoginResponse;
import com.msa.mainserver.util.BcryptUtil;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@Service
@RequiredArgsConstructor
@Transactional(readOnly = true)
@Slf4j
public class UserServiceImpl implements UserService{

	private final UserRepository userRepository;
	private final UserActivityRepository userActivityRepository;
	private final UserAmountRepository userAmountRepository;
	private final BcryptUtil bcryptUtil;
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
	}

	@Override
	public void checkDuplicateInfo(CheckDuplicateRequest request) {

		CheckDuplicateType type = request.getType();

		switch(type){
			case EMAIL:
				Optional<User> findByEmail = userRepository.findByEmail(request.getInfo());
				if(findByEmail.isPresent())
					throw new CustomException(CustomExceptionType.DUPLICATE_EMAIL_EXCEPTION);
				break;
			case NICKNAME:
				Optional<User> findByNickname = userRepository.findByNickname(request.getInfo());
				if(findByNickname.isPresent())
					throw new CustomException(CustomExceptionType.DUPLICATE_NICKNAME_EXCEPTION);
				break;
		}

	}

	@Override
	@Transactional
	public LoginResponse userLogin(LoginRequest request, HttpServletRequest httpRequest) {
		Optional<User> findByEmail = userRepository.findByEmail(request.getEmail());
		log.info(findByEmail.get().getId()+" ");

		if(!findByEmail.isPresent())
			throw new CustomException(CustomExceptionType.USER_NOT_FOUND);

		Optional<UserActivity> findByUserEmail = userActivityRepository.findByUser_Email(request.getEmail());

		if(!findByUserEmail.isPresent()){
			UserActivity userActivity = UserActivity.builder()
				.id(findByEmail.get().getId())
				.user(findByEmail.get())
				.recentLoginTime(LocalDateTime.now())
				.recentLoginIp(getClientIp(httpRequest))
				.shortestEscapeTime(null)
				.longestSurvivalTime(null)
				.monsterKills(0)
				.deathCount(0)
				.totalQuestCompleted(0)
				.totalItemCrafted(0)
				.totalEscapeCount(0)
				.totalPlayTime(0)
				.build();

			userActivityRepository.save(userActivity);
		}else{
			findByUserEmail.get().setRecentLoginIp(getClientIp(httpRequest));
			findByUserEmail.get().setRecentLoginTime(LocalDateTime.now());
		}

		log.info("여기서 멈추냐");

		boolean isCanLogin = bcryptUtil.checkPassword(request.getPassword(), findByEmail.get().getPassword());

		if(!isCanLogin)
			throw new CustomException(CustomExceptionType.WRONG_PASSWORD_EXCEPTION);

		Optional<UserAmount> findUserAmount = userAmountRepository.findById(findByEmail.get().getId());

		LoginResponse response = LoginResponse.builder()
			.id(findByEmail.get().getId())
			.nickname(findByEmail.get().getNickname())
			.build();

		if(!findUserAmount.isPresent()){
			UserAmount userAmount = UserAmount.builder()
				.id(findByEmail.get().getId())
				.userAmount(0)
				.user(findByEmail.get())
				.build();
			userAmountRepository.save(userAmount);
			response.setAmount(0);
		}else{
			response.setAmount(findUserAmount.get().getUserAmount());
		}

		return response;
	}

	@Override
	@Transactional
	public void withdrawalUser(WithdrawalUserRequest request) {
		Optional<User> findById = userRepository.findById(request.getId());

		if(!findById.isPresent())
			throw new CustomException(CustomExceptionType.USER_NOT_FOUND);

		findById.get().withdrawalUser();
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
