package com.exodia.schedulerserver.db.repository;

import org.springframework.data.mongodb.repository.MongoRepository;
import com.exodia.schedulerserver.db.entity.InGameQuestLog;

public interface InGameQuestLogRepository extends MongoRepository<InGameQuestLog, Long> {
	int countByUserIdAndGameId(Long userId, String gameId);
}
