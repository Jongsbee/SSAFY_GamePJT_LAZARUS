package com.exodia.logserver.db.repository;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.exodia.logserver.db.entity.InGameQuestLog;

public interface InGameQuestLogRepository extends MongoRepository<InGameQuestLog, Long> {
}
