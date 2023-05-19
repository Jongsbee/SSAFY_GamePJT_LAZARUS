package com.exodia.schedulerserver.db.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.exodia.schedulerserver.db.entity.MonsterStatistics;

public interface MonsterStatisticsRepository extends JpaRepository<MonsterStatistics, Long> {
}
