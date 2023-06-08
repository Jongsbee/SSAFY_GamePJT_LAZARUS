package com.exodia.logserver.db.repository;

import com.exodia.logserver.db.entity.InGameEatLog;
import org.springframework.data.mongodb.repository.MongoRepository;

import java.math.BigInteger;

public interface InGameEatRepository extends MongoRepository<InGameEatLog, BigInteger> {
}
