package com.exodia.schedulerserver.db.repository;

import java.math.BigInteger;
import java.util.List;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.exodia.schedulerserver.db.entity.InGameCraftLog;
import com.exodia.schedulerserver.db.entity.InGameHuntLog;

public interface InGameHuntLogRepository extends MongoRepository<InGameHuntLog, BigInteger> {
	List<InGameHuntLog> findByUserIdAndGameId(Long userId, String gameId);
}
