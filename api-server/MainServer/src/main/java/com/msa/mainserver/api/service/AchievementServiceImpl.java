package com.msa.mainserver.api.service;

import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;

import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.msa.mainserver.common.exception.CustomException;
import com.msa.mainserver.common.exception.CustomExceptionType;
import com.msa.mainserver.db.entity.Achievement;
import com.msa.mainserver.db.entity.User;
import com.msa.mainserver.db.entity.UserAchievement;
import com.msa.mainserver.db.repository.AchievementRepository;
import com.msa.mainserver.db.repository.UserAchievementRepository;
import com.msa.mainserver.db.repository.UserRepository;
import com.msa.mainserver.dto.request.AchievementUpdateRequest;
import com.msa.mainserver.dto.response.AchievementResponse;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@Service
@RequiredArgsConstructor
@Transactional(readOnly = true)
@Slf4j
public class AchievementServiceImpl implements AchievementService {
	private final UserRepository userRepository;

	private final UserAchievementRepository userAchievementRepository;
	private final AchievementRepository achievementRepository;

	@Override
	public List<AchievementResponse> getAchievements(long userId) {

		List<UserAchievement> userAchievements = userAchievementRepository.findAllByUserId(userId);

		if (userAchievements.isEmpty())
			throw new CustomException(CustomExceptionType.USER_ACHIEVEMENT_NOT_FOUND);

		List<AchievementResponse> achievementResponses = userAchievements.stream()
			.map(AchievementResponse::fromUserAchievement).collect(Collectors.toList());

		return achievementResponses;
	}

	@Override
	@Transactional
	public void updateAchievement(List<AchievementUpdateRequest> requestList) {

		for (AchievementUpdateRequest request : requestList) {
			UserAchievement userAchievement = userAchievementRepository.findByUserIdAndAchievementId(
				request.getUserId(),
				request.getAchievementId());

			User user = userRepository.findById(request.getUserId()).orElseThrow(
				() -> new CustomException(CustomExceptionType.USER_NOT_FOUND));

			Achievement achievement = achievementRepository.findById(request.getAchievementId()).orElseThrow(
				() -> new CustomException(CustomExceptionType.ACHIEVEMENT_NOT_FOUND));

			if (userAchievement == null) {
				userAchievement = UserAchievement.builder()
					.user(user)
					.achievement(achievement)
					.achievementDone(false)
					.userAchievementDate(null)
					.build();

				userAchievementRepository.save(userAchievement);
			}

			if (request.getAchievementProgress() < achievement.getAchievementCondition()) {
				userAchievement.setAchievementProgress(request.getAchievementProgress());
			} else {
				userAchievement.setAchievementProgress(achievement.getAchievementCondition())
					.setAchievementDone(true)
					.setUserAchievementDate(LocalDateTime.now());
			}

		}

	}

}
