package com.exodia.logserver.db.repository;


import java.math.BigInteger;

import org.springframework.data.mongodb.repository.MongoRepository;
import com.exodia.logserver.db.entity.InGameCraftLog;

public interface InGameCraftLogRepository extends MongoRepository<InGameCraftLog, BigInteger> {
}
