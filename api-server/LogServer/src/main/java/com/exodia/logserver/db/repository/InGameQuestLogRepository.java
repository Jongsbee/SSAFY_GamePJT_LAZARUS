package com.exodia.logserver.db.repository;

import java.math.BigInteger;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.exodia.logserver.db.entity.InGameQuestLog;

public interface InGameQuestLogRepository extends MongoRepository<InGameQuestLog, BigInteger> {
}
