package com.exodia.logserver.db.repository;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.exodia.logserver.db.entity.GameInfo;

public interface GameInfoRepository extends MongoRepository<GameInfo, String> {
}
