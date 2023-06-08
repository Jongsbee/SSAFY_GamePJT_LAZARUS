package com.msa.mainserver.db.repository;

import java.util.List;
import java.util.Optional;

import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import com.msa.mainserver.db.entity.UserActivity;

public interface UserActivityRepository extends JpaRepository<UserActivity, Long> {

	Optional<UserActivity> findByUserEmail(String email);
	@Query("SELECT u FROM UserActivity u WHERE u.shortestEscapeTime IS NOT NULL ORDER BY u.shortestEscapeTime ASC")
	List<UserActivity> findTop3ShortestEscape(Pageable pageable);

	@Query("SELECT u FROM UserActivity u ORDER BY (u.normalMonsterKills + u.eliteMonsterKills) DESC")
	List<UserActivity> findTop3MonsterKill(Pageable pageable);

	@Query("SELECT u FROM UserActivity u ORDER BY u.totalItemCrafted DESC ")
	List<UserActivity> findTop3ItemCraft(Pageable pageable);

	@Query("SELECT u FROM UserActivity u ORDER BY u.totalQuestCompleted DESC ")
	List<UserActivity> findTop3QuestCleared(Pageable pageable);
}
