package com.msa.mainserver.dto.request;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class AchievementUpdateRequest {
	private long userId;
	private long achievementId;
	private int achievementProgress;
}
