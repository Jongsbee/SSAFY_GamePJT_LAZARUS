package com.msa.mainserver.dto.request;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class RegisterRequest {
	private String email;
	private String password;
	private String nickname;
}
