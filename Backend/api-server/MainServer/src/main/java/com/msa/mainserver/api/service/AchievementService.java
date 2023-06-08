package com.msa.mainserver.api.service;

import java.util.List;

import com.msa.mainserver.dto.request.AchievementUpdateRequest;
import com.msa.mainserver.dto.response.AchievementResponse;

public interface AchievementService {
	List<AchievementResponse> getAchievements(long userId);

	void updateAchievement(List<AchievementUpdateRequest> requestList);
}
