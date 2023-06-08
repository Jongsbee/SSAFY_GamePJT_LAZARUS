package com.exodia.logserver.dto.request;

import java.util.ArrayList;
import java.util.List;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class StartRoomRequest {

	private String gameId;
	private Long users;

}
