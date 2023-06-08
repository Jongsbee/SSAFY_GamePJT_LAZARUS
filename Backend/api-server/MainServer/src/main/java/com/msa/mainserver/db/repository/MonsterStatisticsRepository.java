package com.msa.mainserver.db.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import com.msa.mainserver.db.entity.MonsterStatistics;

public interface MonsterStatisticsRepository extends JpaRepository<MonsterStatistics, Long> {
	@Query("SELECT ms.monsterKilled FROM MonsterStatistics ms ORDER BY ms.id ASC")
	List<Integer> findMonsterKilled();
}
