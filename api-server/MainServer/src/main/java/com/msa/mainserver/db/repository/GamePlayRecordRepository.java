package com.msa.mainserver.db.repository;

import com.msa.mainserver.db.entity.GameplayRecord;
import org.springframework.data.jpa.repository.JpaRepository;

public interface GamePlayRecordRepository extends JpaRepository<GameplayRecord, Long> {
}
