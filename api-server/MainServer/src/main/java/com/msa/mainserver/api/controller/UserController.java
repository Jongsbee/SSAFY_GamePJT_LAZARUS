package com.msa.mainserver.api.controller;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;

import com.msa.mainserver.dto.request.*;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import com.msa.mainserver.api.service.UserService;
import com.msa.mainserver.db.entity.User;
import com.msa.mainserver.dto.response.LoginResponse;
import com.msa.mainserver.util.BcryptUtil;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@RestController
@RequiredArgsConstructor
@Slf4j
@RequestMapping("/users")
@Tag(name = "유저", description = "유저 관련 api 입니다.")
public class UserController {

	private final UserService userService;

	/**
	 * 회원가입 메소드
	 * @param registerRequest => 회원가입 진행하기위한 정보가 들어있는 파라미터 ( email , password , nickname )
	 *                         	 PASSWORD 는 Bcrypt 를 통해 Encoding
	 */
	@PostMapping("/register")
	@Operation(summary = "회원가입", description = "회원가입 메서드.")
	public void userRegister(@RequestBody RegisterRequest registerRequest) {
		userService.userRegister(registerRequest);
	}

	/**
	 * 중복된 정보가 있는지 확인하기 위한 메소드
	 * @param request => 중복된 정보가 있는지 확인하기 위한 파라미터로 다음과 같은 정보가 들어있음
	 *                    TYPE : 중복된 정보가 어떤 타입인지 ( ex : EMAIL, NICKNAME )
	 *                    INFO : 실제로 중복되었는지 확인하기 위한 값
	 * @return => 중복된 정보가 없다면 HttpRequest code 200을 리턴 중복되었다면 예외처리를 통해 409 Return
	 */
	@PostMapping("/check/duplicate")
	@Operation(summary = "중복체크", description = "중복체크 메서드.")
	public ResponseEntity checkDuplicateInfo(@RequestBody CheckDuplicateRequest request){
		userService.checkDuplicateInfo(request);
		return ResponseEntity.ok("사용 가능");
	}

	/**
	 * 로그인을 진행하기 위한 메소드
	 * @param loginRequest =>  로그인을 진행하기 위한 정보가 들어있는 파라미터 (EMAIL, PW)
	 * @return 로그인이 정상적으로 진행되었으면 Login Response를 반환 ( id, nickname, amount )
	 */
	@PostMapping("/login")
	@Operation(summary = "로그인", description = "로그인 메서드.")
	public ResponseEntity userLogin(@RequestBody LoginRequest loginRequest, HttpServletRequest httpRequest){
		LoginResponse response = userService.userLogin(loginRequest, httpRequest);
		return ResponseEntity.ok(response);
	}

	/**
	 * 회원탈퇴를 진행하기 위한 메소드
	 * @param request => 회원의 PK를 파라미터로 받아 회원탈퇴를 진행
	 * @return => 회원탈퇴가 정상적으로 진행되었으면 HttpStatus 200을 반환
	 */
	@PostMapping("/withdrawal")
	@Operation(summary = "회원탈퇴", description = "회원탈퇴 메서드.")
	public ResponseEntity withdrawalUser(@RequestBody WithdrawalUserRequest request){
		userService.withdrawalUser(request);
		return ResponseEntity.ok("회원탈퇴가 정상적으로 완료되었습니다");
	}

	/**
	 * EMAIL 인증을 위한 메소드
	 * @param email => EMAIl을 파라미터로 받는다
	 * @return => EMAIL 전송을 성공적으로 완료 시 return HttpsStatus 200
	 */
	@GetMapping("/verify/{email}")
	@Operation(summary = "메일인증 요청", description = "메일인증 요청 메서드.")
	public ResponseEntity sendVerifyEmail(@PathVariable String email){
		userService.sendVerificationMail(email);
		return ResponseEntity.ok("인증 메일 발송 완료");
	}

	@GetMapping("/verify/request/{uuid}")
	public String getVerifyEmailCode(@PathVariable String uuid){
		String verifyEmail = userService.getVerifyEmail(uuid);
		return verifyEmail;
	}







}
