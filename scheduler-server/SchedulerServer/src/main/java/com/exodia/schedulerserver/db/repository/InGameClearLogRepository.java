package com.exodia.schedulerserver.db.repository;

import java.math.BigInteger;
import java.util.List;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.exodia.schedulerserver.db.entity.InGameClearLog;
import com.exodia.schedulerserver.db.entity.InGameCraftLog;

public interface InGameClearLogRepository extends MongoRepository<InGameClearLog, BigInteger> {

	InGameClearLog findByUserIdAndGameId(Long userId, String gameId);
}
