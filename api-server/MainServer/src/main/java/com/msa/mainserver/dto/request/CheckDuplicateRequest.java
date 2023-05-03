package com.msa.mainserver.dto.request;

import com.msa.mainserver.dto.enums.CheckDuplicateType;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class CheckDuplicateRequest {
	private String info;
	private CheckDuplicateType type;
}
