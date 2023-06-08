package com.exodia.logserver.db.repository;

import java.math.BigInteger;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.exodia.logserver.db.entity.InGameUseLog;

public interface InGameUseLogRepository extends MongoRepository<InGameUseLog, BigInteger> {
}
