package com.exodia.logserver.dto.request;

import com.exodia.logserver.dto.enums.CreatureType;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class HuntLogRequest {

	private Long userId;
	private Long creatureId;
	private CreatureType creatureType;
	private String gameId;
}
