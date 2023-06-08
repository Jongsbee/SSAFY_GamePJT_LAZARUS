package com.msa.mainserver.db.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;

import com.msa.mainserver.db.entity.UserAchievement;

public interface UserAchievementRepository extends JpaRepository<UserAchievement, Long> {

	List<UserAchievement> findAllByUserId(long userId);

	UserAchievement findByUserIdAndAchievementId(long userId, long achievementId);
}