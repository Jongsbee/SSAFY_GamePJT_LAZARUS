package com.exodia.schedulerserver.db.repository;


import java.math.BigInteger;
import java.util.List;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.exodia.schedulerserver.db.entity.InGameCraftLog;

public interface InGameCraftLogRepository extends MongoRepository<InGameCraftLog, BigInteger> {

	int countByUserIdAndGameId(Long userId, String gameId);

	List<InGameCraftLog> findInGameCraftLogsByGameId(String gameId);
}
