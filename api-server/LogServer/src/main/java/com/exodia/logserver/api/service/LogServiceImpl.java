package com.exodia.logserver.api.service;

import java.time.LocalDateTime;
import java.time.temporal.ChronoUnit;
import java.util.Optional;

import com.exodia.logserver.db.entity.*;
import com.exodia.logserver.db.repository.*;
import com.exodia.logserver.dto.request.*;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.exodia.logserver.common.exception.CustomException;
import com.exodia.logserver.common.exception.CustomExceptionType;
import com.exodia.logserver.dto.enums.GameStatus;

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
	private final InGameQuestLogRepository inGameQuestLogRepository;
	private final InGameUseLogRepository inGameUseLogRepository;
	private final InGameEatRepository inGameEatRepository;

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
	public void saveUseLog(UseLogRequest useLogRequest) {
		InGameUseLog inGameUseLog = InGameUseLog.builder()
			.gameId(useLogRequest.getGameId())
			.userId(useLogRequest.getUserId())
			.itemId(useLogRequest.getItemId())
			.useTime(LocalDateTime.now())
			.build();

		inGameUseLogRepository.save(inGameUseLog);
	}

	@Override
	public void saveEatLog(EatLogRequest eatLogRequest) {
		InGameEatLog inGameEatLog = InGameEatLog.builder()
				.gameId(eatLogRequest.getGameId())
				.userId(eatLogRequest.getUserId())
				.itemId(eatLogRequest.getItemId())
				.useTime(LocalDateTime.now())
				.build();

		inGameEatRepository.save(inGameEatLog);

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
		//log.info("cleared? = " + request.isCleared());
		Optional<GameInfo> gameInfo = gameInfoRepository.findById(request.getGameId());
		if(!gameInfo.isPresent())
			throw new CustomException(CustomExceptionType.GAME_NOT_FOUND);

		if(request.isCleared()){

			// 클리어까지 걸린 시간
			Long spentTime = gameInfo.get().getStartTime().until(LocalDateTime.now(), ChronoUnit.SECONDS);

			inGameClearLog = InGameClearLog.builder()
				.gameId(request.getGameId())
				.userId(request.getUserId())
				.cleared(request.isCleared())
				.endTime(LocalDateTime.now())
				.spentTime(spentTime)
				.build();

			gameInfo.get().setEndTime(LocalDateTime.now());
			gameInfo.get().setGameStatus(GameStatus.NORMAL_END);
			gameInfoRepository.save(gameInfo.get());
		}else{
			Long spentTime = gameInfo.get().getStartTime().until(LocalDateTime.now(), ChronoUnit.SECONDS);

			inGameClearLog = InGameClearLog.builder()
				.gameId(request.getGameId())
				.userId(request.getUserId())
				.cleared(request.isCleared())
				.endTime(LocalDateTime.now())
				.spentTime(spentTime)
				.build();
		}

		inGameClearLogRepository.save(inGameClearLog);
	}

	@Override
	public void saveQuestLog(QuestLogRequest request) {
		InGameQuestLog inGameQuestLog = new InGameQuestLog().builder()
			.questId(request.getQuestId())
			.userId(request.getUserId())
			.gameId(request.getGameId())
			.clearTime(LocalDateTime.now())
			.build();

		inGameQuestLogRepository.save(inGameQuestLog);
	}


}
