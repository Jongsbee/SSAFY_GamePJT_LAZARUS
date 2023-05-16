package com.exodia.schedulerserver.db.repository;

import java.math.BigInteger;
import java.util.List;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.exodia.schedulerserver.db.entity.InGameUseLog;

public interface InGameUseLogRepository extends MongoRepository<InGameUseLog, BigInteger> {
	int countByUserIdAndGameId(Long userId, String gameId);
	List<InGameUseLog> findInGameUseLogsByGameId(String gameId);
}
