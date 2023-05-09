package com.exodia.logserver.db.repository;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.exodia.logserver.db.entity.InGameHuntLog;

public interface InGameHuntLogRepository extends MongoRepository<InGameHuntLog, Long> {
}
