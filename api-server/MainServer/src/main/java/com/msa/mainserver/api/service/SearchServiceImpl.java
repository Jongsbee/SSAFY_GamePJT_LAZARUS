package com.msa.mainserver.api.service;

import com.msa.mainserver.common.exception.CustomException;
import com.msa.mainserver.common.exception.CustomExceptionType;
import com.msa.mainserver.db.entity.User;
import com.msa.mainserver.db.entity.UserActivity;
import com.msa.mainserver.db.repository.UserActivityRepository;
import com.msa.mainserver.db.repository.UserRepository;
import com.msa.mainserver.dto.response.FindUserResponse;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Optional;

@Service
@Slf4j
@RequiredArgsConstructor
@Transactional(readOnly = true)
public class SearchServiceImpl implements SearchService{

    private final UserRepository userRepository;
    private final UserActivityRepository userActivityRepository;
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
}
