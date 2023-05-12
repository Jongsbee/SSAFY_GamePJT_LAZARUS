package com.msa.mainserver.common.exception;

import org.springframework.http.HttpStatus;

public enum CustomExceptionType {
	RUNTIME_EXCEPTION(HttpStatus.BAD_REQUEST, "E001", "잘못된 요청입니다."),
	INTERNAL_SERVER_ERROR(HttpStatus.INTERNAL_SERVER_ERROR, "E002", "서버 오류 입니다."),
	USER_NOT_FOUND(HttpStatus.NOT_FOUND, "E003", "존재하지 않은 사용자입니다"),
	WRONG_PASSWORD_EXCEPTION(HttpStatus.UNAUTHORIZED, "E004", "잘못된 패스워드 입니다"),
	DUPLICATE_EMAIL_EXCEPTION(HttpStatus.CONFLICT, "E005", "이미 사용되고 있는 이메일입니다"),
	DUPLICATE_NICKNAME_EXCEPTION(HttpStatus.CONFLICT, "E006", "이미 사용되고 있는 닉네임 입니다"),
	NOT_LOGINED_EXCEPTION(HttpStatus.BAD_REQUEST, "E007", "로그인을 진행한 사용자가 아닙니다"),
	UN_VERIFICATION_EXCEPTION(HttpStatus.UNAUTHORIZED, "E008", "아직 이메일 인증을 진행하지 않았습니다"),
	ACHIEVEMENT_NOT_FOUND(HttpStatus.NOT_FOUND, "E009", "해당 업적이 존재하지 않습니다"),
	USER_ACHIEVEMENT_NOT_FOUND(HttpStatus.NOT_FOUND, "E010", "해당 유저의 업적이 조회되지 않습니다");

	private final HttpStatus httpStatus;
	private final String code;
	private String message;

	CustomExceptionType(HttpStatus httpStatus, String code) {
		this.httpStatus = httpStatus;
		this.code = code;
	}

	CustomExceptionType(HttpStatus httpStatus, String code, String message) {
		this.httpStatus = httpStatus;
		this.code = code;
		this.message = message;
	}

	public HttpStatus getHttpStatus() {
		return httpStatus;
	}

	public String getCode() {
		return code;
	}

	public String getMessage() {
		return message;
	}
}

