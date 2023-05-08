package com.exodia.logserver.api.service;

import org.springframework.stereotype.Service;

import com.exodia.logserver.db.repository.GameInfoRepository;
import com.exodia.logserver.dto.Request.CreateRoomRequest;
import com.exodia.logserver.dto.response.CreateRoomResponse;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@Service
@RequiredArgsConstructor
@Slf4j
public class GameInfoServiceImpl implements GameInfoService{
	private final GameInfoRepository gameInfoRepository;

	@Override
	public CreateRoomResponse createRoom(CreateRoomRequest request){

		return null;
	}
}
