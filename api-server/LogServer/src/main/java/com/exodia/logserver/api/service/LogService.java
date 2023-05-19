package com.exodia.logserver.api.service;

import com.exodia.logserver.dto.request.*;

public interface LogService {

	public void saveCraftLog(CraftLogRequest request);
	public void saveHuntLog(HuntLogRequest request);
	public void saveClearLog(ClearLogRequest request);
	public void saveQuestLog(QuestLogRequest request);
	public void saveUseLog(UseLogRequest useLogRequest);
	public void saveEatLog(EatLogRequest eatLogRequest);
}
