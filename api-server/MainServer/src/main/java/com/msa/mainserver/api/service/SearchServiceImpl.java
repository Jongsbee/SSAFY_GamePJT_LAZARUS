package com.msa.mainserver.api.service;

import com.msa.mainserver.common.exception.CustomException;
import com.msa.mainserver.common.exception.CustomExceptionType;
import com.msa.mainserver.db.entity.GameplayRecord;
import com.msa.mainserver.db.entity.User;
import com.msa.mainserver.db.entity.UserActivity;
import com.msa.mainserver.db.repository.GamePlayRecordRepository;
import com.msa.mainserver.db.repository.UserActivityRepository;
import com.msa.mainserver.db.repository.UserRepository;
import com.msa.mainserver.dto.response.FindRecordResponse;
import com.msa.mainserver.dto.response.FindUserResponse;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.Duration;
import java.time.LocalDateTime;
import java.time.temporal.ChronoUnit;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
@Slf4j
@RequiredArgsConstructor
@Transactional(readOnly = true)
public class SearchServiceImpl implements SearchService{

    private final UserRepository userRepository;
    private final UserActivityRepository userActivityRepository;
    private final GamePlayRecordRepository gamePlayRecordRepository;
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
