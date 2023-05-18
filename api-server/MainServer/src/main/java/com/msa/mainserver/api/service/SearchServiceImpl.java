package com.msa.mainserver.api.service;

import com.msa.mainserver.common.exception.CustomException;
import com.msa.mainserver.common.exception.CustomExceptionType;
import com.msa.mainserver.db.entity.GameplayRecord;
import com.msa.mainserver.db.entity.User;
import com.msa.mainserver.db.entity.UserActivity;
import com.msa.mainserver.db.repository.GamePlayRecordRepository;
import com.msa.mainserver.db.repository.ItemStatisticRepository;
import com.msa.mainserver.db.repository.MonsterStatisticsRepository;
import com.msa.mainserver.db.repository.UserActivityRepository;
import com.msa.mainserver.db.repository.UserRepository;
import com.msa.mainserver.dto.CraftRankDto;
import com.msa.mainserver.dto.EscapeRankDto;
import com.msa.mainserver.dto.HuntRankDto;
import com.msa.mainserver.dto.QuestRankDto;
import com.msa.mainserver.dto.response.FindRecordResponse;
import com.msa.mainserver.dto.response.FindUserResponse;
import com.msa.mainserver.dto.response.RankingResponse;

import com.msa.mainserver.dto.response.StatisticsResponse;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import javax.annotation.PostConstruct;
import java.time.Duration;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.time.temporal.ChronoUnit;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
@Slf4j
@RequiredArgsConstructor
@Transactional(readOnly = true)
public class SearchServiceImpl implements SearchService{

    private final ItemStatisticRepository itemStatisticRepository;
    private final UserRepository userRepository;
    private final UserActivityRepository userActivityRepository;
    private final GamePlayRecordRepository gamePlayRecordRepository;
    private final MonsterStatisticsRepository monsterStatisticsRepository;
    private final Long[] craftIds = {60L, 61L, 100L, 101L, 110L, 111L, 120L, 121L};
    private final Long[] usedIds = {30L, 31L, 32L, 40L, 41L, 42L, 200L, 201L, 202L, 210L, 211L, 212L};
    private List<Long> craftItemIdList;
    private List<Long> usedItemIdList;

    @PostConstruct
    public void init(){
        craftItemIdList = new ArrayList<>();
        usedItemIdList = new ArrayList<>();

        for(Long id : craftIds){
            craftItemIdList.add(id);
        }
        for(Long id : usedIds){
            usedItemIdList.add(id);
        }
    }

    @Override
    public FindUserResponse findUserActivity(String nickname) {
        Optional<User> findUser = userRepository.findByNickname(nickname);
        if(!findUser.isPresent())
            throw new CustomException(CustomExceptionType.USER_NOT_FOUND);

        Optional<UserActivity> findUserActivity = userActivityRepository.findById(findUser.get().getId());

        FindUserResponse findUserResponse = FindUserResponse.builder()
                .shortestEscapeTime(findUserActivity.get().getShortestEscapeTime())
                .longestSurvivalTime(findUserActivity.get().getLongestSurvivalTime())
                .normalMonsterKills(findUserActivity.get().getNormalMonsterKills())
                .eliteMonsterKills(findUserActivity.get().getEliteMonsterKills())
                .totalQuestCompleted(findUserActivity.get().getTotalQuestCompleted())
                .totalItemCrafted(findUserActivity.get().getTotalItemCrafted())
                .deathCount(findUserActivity.get().getDeathCount())
                .totalEscapeCount(findUserActivity.get().getTotalEscapeCount())
                .totalPlayTime(findUserActivity.get().getTotalPlayTime())
                .build();

        return findUserResponse;
    }

    @Override
    public List<FindRecordResponse> findUserRecord(String nickname, int page) {
        Pageable pageable = PageRequest.of(page, 10, Sort.by("gameEndTime").descending());
        Optional<User> findUser = userRepository.findByNickname(nickname);
        if(!findUser.isPresent())
            throw  new CustomException(CustomExceptionType.USER_NOT_FOUND);

        Page<GameplayRecord> GameRecords = gamePlayRecordRepository.findByUserOrderByGameEndTimeDesc(
            findUser.get(), pageable);

        List<FindRecordResponse> recordResponses = new ArrayList<>();

        for(GameplayRecord gpr : GameRecords.getContent()){
            String when = getTimeDifference(gpr.getGameEndTime());
            String gameTime = formatDuration(gpr.getSpentTime());
            String result = "";
            if(gpr.isEscapeFlag()){
                result = "탈출";
            }else{
                result = "사망";
            }

            FindRecordResponse findRecordResponse = FindRecordResponse.builder()
                .result(result)
                .normal(gpr.getKillMonsterCount())
                .elite(gpr.getKillEliteCount())
                .item(gpr.getRecordItemTotalCraft())
                .quest(gpr.getQuestCompletedCount())
                .gameTime(gameTime)
                .when(when)
                .build();

            recordResponses.add(findRecordResponse);
        }

        return recordResponses;
    }

    @Override
    public RankingResponse getRanking() {
        Pageable topThree = PageRequest.of(0, 3);
        List<UserActivity> top3ShortestEscape = userActivityRepository.findTop3ShortestEscape(topThree);
        List<UserActivity> top3MonsterKill = userActivityRepository.findTop3MonsterKill(topThree);
        List<UserActivity> top3ItemCraft = userActivityRepository.findTop3ItemCraft(topThree);
        List<UserActivity> top3QuestCleared = userActivityRepository.findTop3QuestCleared(topThree);

        List<EscapeRankDto> escapeRankDtoList = top3ShortestEscape.stream().map(escape -> new EscapeRankDto().builder()
            .nickname(escape.getUser().getNickname())
            .time(formatDuration(escape.getShortestEscapeTime()))
            .build()).collect(Collectors.toList());

        List<HuntRankDto> huntRankDtoList = top3MonsterKill.stream().map(hunt -> new HuntRankDto().builder()
            .nickname(hunt.getUser().getNickname())
            .cnt(hunt.getNormalMonsterKills() + hunt.getEliteMonsterKills())
            .build()).collect(Collectors.toList());

        List<CraftRankDto> craftRankDtoList = top3ItemCraft.stream().map(craft -> new CraftRankDto().builder()
            .nickname(craft.getUser().getNickname())
            .cnt(craft.getTotalItemCrafted())
            .build()).collect(Collectors.toList());

        List<QuestRankDto> questRankDtoList = top3QuestCleared.stream().map(quest -> new QuestRankDto().builder()
            .nickname(quest.getUser().getNickname())
            .cnt(quest.getTotalQuestCompleted())
            .build()).collect(Collectors.toList());

        RankingResponse rankingResponse = RankingResponse.builder()
            .escapeRanks(escapeRankDtoList)
            .craftRanks(craftRankDtoList)
            .huntRanks(huntRankDtoList)
            .questRanks(questRankDtoList)
            .build();

        return rankingResponse;
    }

    @Override
    public StatisticsResponse getStatistics() {
        Pageable pageable = PageRequest.of(0, 10);

        List<Integer> itemTotalCraftList = itemStatisticRepository.findItemTotalCraft(craftItemIdList);
        List<Integer> itemTotalUsedList = itemStatisticRepository.findItemTotalUsed(usedItemIdList);
        List<Integer> monsterKilledList = monsterStatisticsRepository.findMonsterKilled();

        Page<GameplayRecord> recentInfoLists = gamePlayRecordRepository.findRecentSpentTimeAndGameEndTime(pageable);

        List<String> timeLists = new ArrayList<>();
        List<Long> spentTimes = new ArrayList<>();

        for(GameplayRecord gr : recentInfoLists){
            timeLists.add(formatDate(gr.getGameEndTime()));
            spentTimes.add(gr.getSpentTime());
        }

        StatisticsResponse response = StatisticsResponse.builder()
                .craftItemList(itemTotalCraftList)
                .useFoodList(itemTotalUsedList)
                .whenList(timeLists)
                .huntedMonsterList(monsterKilledList)
                .spentTimeList(spentTimes)
                .build();


        return response;
    }

    public String formatDate(LocalDateTime time){
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("MM.dd.HH:mm");
        return time.format(formatter);
    }

    public String formatDuration(long seconds) {
        long hours = seconds / 3600;
        long minutes = (seconds % 3600) / 60;
        long secs = seconds % 60;

        if (hours > 0) {
            return String.format("%d시간 %d분 %d초", hours, minutes, secs);
        } else if (minutes > 0) {
            return String.format("%d분 %d초", minutes, secs);
        } else {
            return String.format("%d초", secs);
        }
    }

    public String getTimeDifference(LocalDateTime gameEndTime) {
        LocalDateTime now = LocalDateTime.now();
        Duration duration = Duration.between(gameEndTime, now);

        long years = ChronoUnit.YEARS.between(gameEndTime, now);
        if (years > 0) {
            return years + "년";
        }

        long days = duration.toDays();
        if (days > 0) {
            return days + "일";
        }

        long hours = duration.toHours();
        if (hours > 0) {
            return hours + "시간";
        }

        long minutes = duration.toMinutes();
        return minutes + "분";
    }
}
