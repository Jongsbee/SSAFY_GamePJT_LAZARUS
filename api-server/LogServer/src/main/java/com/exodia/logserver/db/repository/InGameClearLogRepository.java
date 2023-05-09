package com.exodia.logserver.db.repository;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.exodia.logserver.db.entity.InGameClearLog;

public interface InGameClearLogRepository extends MongoRepository<InGameClearLog, Long> {
}
