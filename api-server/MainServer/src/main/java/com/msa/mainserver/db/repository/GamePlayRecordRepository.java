package com.msa.mainserver.db.repository;

import com.msa.mainserver.db.entity.GameplayRecord;
import com.msa.mainserver.db.entity.User;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;

public interface GamePlayRecordRepository extends JpaRepository<GameplayRecord, Long> {
	Page<GameplayRecord> findByUserOrderByGameEndTimeDesc(User user, Pageable pageable);
}
