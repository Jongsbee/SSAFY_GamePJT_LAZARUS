package com.exodia.logserver.dto.request;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class ClearLogRequest {
	private Long userId;
	private String gameId;
	private boolean cleared;
}
