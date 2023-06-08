package com.exodia.schedulerserver.db.repository;

import com.exodia.schedulerserver.db.entity.InGameCraftLog;
import com.exodia.schedulerserver.db.entity.InGameEatLog;
import org.springframework.data.mongodb.repository.MongoRepository;

import java.math.BigInteger;
import java.util.List;

public interface InGameEatLogRepository extends MongoRepository<InGameEatLog, BigInteger> {
    int countByUserIdAndGameId(Long userId, String gameId);

    List<InGameEatLog> findInGameEatLogsByGameId(String gameId);
}
