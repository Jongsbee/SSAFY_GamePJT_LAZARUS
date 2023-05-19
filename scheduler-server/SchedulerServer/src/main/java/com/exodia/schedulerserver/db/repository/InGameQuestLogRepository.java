package com.exodia.schedulerserver.db.repository;

import org.springframework.data.mongodb.repository.MongoRepository;
import com.exodia.schedulerserver.db.entity.InGameQuestLog;

import java.math.BigInteger;

public interface InGameQuestLogRepository extends MongoRepository<InGameQuestLog, BigInteger> {
	int countByUserIdAndGameId(Long userId, String gameId);
}
