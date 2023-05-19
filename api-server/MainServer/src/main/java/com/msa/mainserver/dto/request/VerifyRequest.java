package com.msa.mainserver.dto.request;

import lombok.Getter;
import lombok.Setter;

import javax.validation.constraints.Email;
import javax.validation.constraints.NotNull;

@Getter
@Setter
public class VerifyRequest {
    @NotNull(message = "email may not be empty")
    @Email(message = "이메일 형식이 아닙니다.")
    private String email;
    @NotNull(message = "인증코드를 입력해야 합니다.")
    private String code;
}
