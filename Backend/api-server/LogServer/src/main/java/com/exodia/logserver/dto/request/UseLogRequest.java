package com.exodia.logserver.dto.request;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class UseLogRequest {
	private Long userId;
	private Long itemId;
	private String gameId;
}
