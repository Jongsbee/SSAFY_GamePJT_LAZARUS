package com.msa.mainserver.db.repository;

import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;

import com.msa.mainserver.db.entity.UserActivity;

public interface UserActivityRepository extends JpaRepository<UserActivity, Long> {

	Optional<UserActivity> findByUserEmail(String email);
}
