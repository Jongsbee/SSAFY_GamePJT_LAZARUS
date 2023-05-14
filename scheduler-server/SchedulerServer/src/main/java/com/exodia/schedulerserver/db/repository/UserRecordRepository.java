package com.exodia.schedulerserver.db.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.exodia.schedulerserver.db.entity.GamePlayRecord;

public interface UserRecordRepository extends JpaRepository<GamePlayRecord, Long> {
}
