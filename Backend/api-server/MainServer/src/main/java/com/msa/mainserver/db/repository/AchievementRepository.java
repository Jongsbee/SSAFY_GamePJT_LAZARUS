package com.msa.mainserver.db.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.msa.mainserver.db.entity.Achievement;

public interface AchievementRepository extends JpaRepository<Achievement, Long> {
}