package com.exodia.logserver.dto.request;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class QuestLogRequest {
	private Long userId;
	private Long questId;
	private String gameId;
}
