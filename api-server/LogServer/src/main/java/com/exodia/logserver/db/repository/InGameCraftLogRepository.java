package com.exodia.logserver.db.repository;


import org.springframework.data.mongodb.repository.MongoRepository;
import com.exodia.logserver.db.entity.InGameCraftLog;

public interface InGameCraftLogRepository extends MongoRepository<InGameCraftLog, Long> {
}
