package com.msa.mainserver.dto.response;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@Builder
@AllArgsConstructor
@NoArgsConstructor
@Schema(description = "성공 응답 DTO")
public class SuccessResponse {
	@Schema(description = "응답 코드", example = "success")
	private final String code = "success";
	@Schema(description = "응답 메시지", example = "성공")
	private String message;
}