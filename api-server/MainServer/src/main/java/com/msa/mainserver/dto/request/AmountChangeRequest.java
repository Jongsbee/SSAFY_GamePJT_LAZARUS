package com.msa.mainserver.dto.request;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class AmountChangeRequest {
	private Long userId;
	private int amount;
}
