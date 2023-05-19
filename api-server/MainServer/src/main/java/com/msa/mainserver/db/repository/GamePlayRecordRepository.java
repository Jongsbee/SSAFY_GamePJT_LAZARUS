package com.msa.mainserver.db.repository;

import com.msa.mainserver.db.entity.GameplayRecord;
import com.msa.mainserver.db.entity.User;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

public interface GamePlayRecordRepository extends JpaRepository<GameplayRecord, Long> {
	Page<GameplayRecord> findByUserOrderByGameEndTimeDesc(User user, Pageable pageable);

	@Query(value = "SELECT r FROM GameplayRecord r ORDER BY r.gameEndTime DESC")
	Page<GameplayRecord> findRecentSpentTimeAndGameEndTime(Pageable pageable);
}
