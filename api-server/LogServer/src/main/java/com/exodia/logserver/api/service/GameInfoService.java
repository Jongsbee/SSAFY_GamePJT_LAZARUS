package com.exodia.logserver.api.service;

import com.exodia.logserver.dto.Request.CreateRoomRequest;
import com.exodia.logserver.dto.response.CreateRoomResponse;

public interface GameInfoService {
	public CreateRoomResponse createRoom(CreateRoomRequest request);
}
