package com.exodia.schedulerserver.db.repository;

import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;

import com.exodia.schedulerserver.db.entity.UserActivity;

public interface UserActivityRepository extends JpaRepository<UserActivity, Long> {

	Optional<UserActivity> findByUser_Email(String email);
}
