package com.exodia.schedulerserver.db.repository;

import java.time.LocalDateTime;
import java.util.List;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.exodia.schedulerserver.db.entity.GameInfo;

public interface GameInfoRepository extends MongoRepository<GameInfo, String> {
	List<GameInfo> findByEndTimeBetween(LocalDateTime start, LocalDateTime end);
}
