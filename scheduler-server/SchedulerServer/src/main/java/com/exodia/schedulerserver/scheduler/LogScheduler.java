package com.exodia.schedulerserver.scheduler;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Optional;

import com.exodia.schedulerserver.db.repository.*;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Component;
import org.springframework.transaction.annotation.Transactional;

import com.exodia.schedulerserver.db.entity.GameInfo;
import com.exodia.schedulerserver.db.entity.InGameClearLog;
import com.exodia.schedulerserver.db.entity.InGameCraftLog;
import com.exodia.schedulerserver.db.entity.InGameHuntLog;
import com.exodia.schedulerserver.db.entity.InGameUseLog;
import com.exodia.schedulerserver.db.entity.ItemStatistic;
import com.exodia.schedulerserver.db.entity.User;
import com.exodia.schedulerserver.db.entity.UserActivity;
import com.exodia.schedulerserver.db.entity.GamePlayRecord;
import com.exodia.schedulerserver.dto.enums.CreatureType;

import lombok.RequiredArgsConstructor;

@Component
@RequiredArgsConstructor
@Transactional
public class  LogScheduler {

	private final GameInfoRepository gameInfoRepository;
	private final InGameClearLogRepository inGameClearLogRepository;
	private final InGameCraftLogRepository inGameCraftLogRepository;
	private final InGameHuntLogRepository inGameHuntLogRepository;
	private final UserActivityRepository userActivityRepository;
	private final UserRepository userRepository;
	private final UserRecordRepository userRecordRepository;
	private final InGameQuestLogRepository inGameQuestLogRepository;
	private final InGameUseLogRepository inGameUseLogRepository;
	private final ItemStatisticRepository itemStatisticRepository;
	private final InGameEatLogRepository inGameEatLogRepository;

	@Scheduled(fixedRate = 3600000)
	public void InsertDataByLog(){

		LocalDateTime yesterday = LocalDateTime.now().minusDays(1);
		LocalDateTime start = yesterday.toLocalDate().atStartOfDay();
		LocalDateTime end = start.plusDays(1);

		List<GameInfo> findGames = gameInfoRepository.findByEndTimeBetween(start, end);

		if(findGames.size() == 0)
			return;

		for(GameInfo gi : findGames){

			for(Long i : gi.getUsers()){
				Optional<User> findUser = userRepository.findById(i);

				//아이템 제작 횟수
				int craftCount = inGameCraftLogRepository.countByUserIdAndGameId(findUser.get().getId(), gi.getId());
				int useCount = inGameUseLogRepository.countByUserIdAndGameId(findUser.get().getId(), gi.getId());
				int eatCount = inGameEatLogRepository.countByUserIdAndGameId(findUser.get().getId(), gi.getId());
				//몬스터 사냥 횟수
				List<InGameHuntLog> huntLogs = inGameHuntLogRepository.findByUserIdAndGameId(
					findUser.get().getId(), gi.getId());

				int eliteCnt = 0;  // 엘리트몹
 				int normalCnt = 0; // 일반 몹
				for(InGameHuntLog log : huntLogs){
					if(log.getCreatureType() == CreatureType.ELITE){
						eliteCnt++;
					}else{
						normalCnt++;
					}
				}
				// 인게임 클리어타임 로그
				InGameClearLog clearLog = inGameClearLogRepository.findByUserIdAndGameId(
					findUser.get().getId(), gi.getId());

				// 총 클리어한 퀫스트 개수
				int questCnt = inGameQuestLogRepository.countByUserIdAndGameId(findUser.get().getId(), gi.getId());

				GamePlayRecord gamePlayRecord = GamePlayRecord.builder()
					.user(findUser.get())
					.gameId(gi.getId())
					.gameEndTime(gi.getEndTime())
					.killEliteCount(eliteCnt)
					.killMonsterCount(normalCnt)
					.itemTotalCraft(craftCount)
					.userEscape(clearLog.isCleared())
					.spentTime(clearLog.getSpentTime())
					.totalQuestClearCnt(questCnt)
					.build();

				userRecordRepository.save(gamePlayRecord);

				Optional<UserActivity> userActivity = userActivityRepository.findById(i);

				//유저 활동 테이블 갱신
				userActivity.get().changeEscapeAndDeathCnt(clearLog.isCleared());
				userActivity.get().checkTimeToChange(clearLog.getSpentTime());
				userActivity.get().increaseMonsterKills(normalCnt, eliteCnt);
				userActivity.get().increaseItemCraftedCnt(craftCount);
				userActivity.get().increaseQuestCompletedCnt(questCnt);
				userActivity.get().increaseItemUseCnt(useCount);
				userActivity.get().increaseItemEatCnt(eatCount);

			}

			List<InGameCraftLog> craftLogs = inGameCraftLogRepository.findInGameCraftLogsByGameId(
				gi.getId());

			for(InGameCraftLog cl : craftLogs){
				Optional<ItemStatistic> itemStatistic = itemStatisticRepository.findById(cl.getItemId());
				itemStatistic.get().increaseTotalCraft();

			}
			List<InGameUseLog> useLogs = inGameUseLogRepository.findInGameUseLogsByGameId(gi.getId());
			for(InGameUseLog ul : useLogs){
				Optional<ItemStatistic> itemStatistic = itemStatisticRepository.findById(ul.getItemId());
				itemStatistic.get().increaseTotalUse();
			}

		}

	}
}
