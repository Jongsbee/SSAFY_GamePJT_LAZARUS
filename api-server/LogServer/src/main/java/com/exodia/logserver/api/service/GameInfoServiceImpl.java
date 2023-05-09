package com.exodia.logserver.api.service;

import java.time.LocalDateTime;

import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.exodia.logserver.db.entity.GameInfo;
import com.exodia.logserver.db.repository.GameInfoRepository;
import com.exodia.logserver.dto.enums.GameStatus;
import com.exodia.logserver.dto.request.StartRoomRequest;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@Service
@RequiredArgsConstructor
@Slf4j
@Transactional
public class GameInfoServiceImpl implements GameInfoService{

	private final GameInfoRepository gameInfoRepository;

	@Override
	public void startGame(StartRoomRequest request) {
		GameInfo gameInfo = GameInfo.builder()
			.id(request.getGameId())
			.users(request.getUsers())
			.gameStatus(GameStatus.IN_PROGRESS)
			.startTime(LocalDateTime.now())
			.endTime(null)
			.build();

		gameInfoRepository.save(gameInfo);
	}
}
