package com.exodia.logserver.api.service;

import java.time.LocalDateTime;
import java.time.temporal.ChronoUnit;
import java.util.Optional;

import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.exodia.logserver.common.exception.CustomException;
import com.exodia.logserver.common.exception.CustomExceptionType;
import com.exodia.logserver.db.entity.GameInfo;
import com.exodia.logserver.db.entity.InGameClearLog;
import com.exodia.logserver.db.entity.InGameCraftLog;
import com.exodia.logserver.db.entity.InGameHuntLog;
import com.exodia.logserver.db.repository.GameInfoRepository;
import com.exodia.logserver.db.repository.InGameClearLogRepository;
import com.exodia.logserver.db.repository.InGameCraftLogRepository;
import com.exodia.logserver.db.repository.InGameHuntLogRepository;
import com.exodia.logserver.dto.request.ClearLogRequest;
import com.exodia.logserver.dto.request.CraftLogRequest;
import com.exodia.logserver.dto.request.HuntLogRequest;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@Service
@RequiredArgsConstructor
@Slf4j
@Transactional
public class LogServiceImpl implements LogService{

	private final InGameClearLogRepository inGameClearLogRepository;
	private final InGameCraftLogRepository inGameCraftLogRepository;
	private final InGameHuntLogRepository inGameHuntLogRepository;
	private final GameInfoRepository gameInfoRepository;

	@Override
	public void saveCraftLog(CraftLogRequest request) {
		InGameCraftLog inGameCraftLog = InGameCraftLog.builder()
			.gameId(request.getGameId())
			.userId(request.getUserId())
			.itemId(request.getItemId())
			.createTime(LocalDateTime.now())
			.build();

		inGameCraftLogRepository.save(inGameCraftLog);
	}

	@Override
	public void saveHuntLog(HuntLogRequest request) {
		InGameHuntLog inGameHuntLog = InGameHuntLog.builder()
			.gameId(request.getGameId())
			.userId(request.getUserId())
			.creatureId(request.getCreatureId())
			.creatureType(request.getCreatureType())
			.huntTime(LocalDateTime.now())
			.build();

		inGameHuntLogRepository.save(inGameHuntLog);
	}

	@Override
	public void saveClearLog(ClearLogRequest request) {
		InGameClearLog inGameClearLog = null;
		if(request.isCleared()){
			Optional<GameInfo> gameInfo = gameInfoRepository.findById(request.getGameId());
			if(!gameInfo.isPresent())
				throw new CustomException(CustomExceptionType.GAME_NOT_FOUND);

			// 클리어까지 걸린 시간
			Long spentTime = gameInfo.get().getStartTime().until(LocalDateTime.now(), ChronoUnit.SECONDS);

			inGameClearLog = InGameClearLog.builder()
				.gameId(request.getGameId())
				.userId(request.getUserId())
				.isCleared(request.isCleared())
				.endTime(LocalDateTime.now())
				.spentTime(spentTime)
				.build();

			gameInfo.get().setEndTime(LocalDateTime.now());
		}else{
			inGameClearLog = InGameClearLog.builder()
				.gameId(request.getGameId())
				.userId(request.getUserId())
				.isCleared(request.isCleared())
				.endTime(null)
				.spentTime(null)
				.build();
		}

		inGameClearLogRepository.save(inGameClearLog);
	}
}
