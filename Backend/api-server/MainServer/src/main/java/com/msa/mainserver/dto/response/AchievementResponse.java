package com.msa.mainserver.dto.response;

import java.time.LocalDateTime;

import com.msa.mainserver.db.entity.UserAchievement;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class AchievementResponse {
	private long achievementId;
	private int achievementProgress;
	private int achievementCondition;
	private boolean achievementDone;
	private LocalDateTime userAchievementDate;
	
	public static AchievementResponse fromUserAchievement(UserAchievement userAchievement) {
		LocalDateTime userAchievementDate =
			userAchievement.isAchievementDone() ? userAchievement.getUserAchievementDate() : null;

		return AchievementResponse.builder()
			.achievementId(userAchievement.getAchievement().getId())
			.achievementProgress(userAchievement.getAchievementProgress())
			.achievementCondition(userAchievement.getAchievement().getAchievementCondition())
			.achievementDone(userAchievement.isAchievementDone())
			.userAchievementDate(userAchievementDate)
			.build();
	}
}