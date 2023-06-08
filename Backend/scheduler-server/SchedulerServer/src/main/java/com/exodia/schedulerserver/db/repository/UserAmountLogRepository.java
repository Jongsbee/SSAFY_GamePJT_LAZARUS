package com.exodia.schedulerserver.db.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.exodia.schedulerserver.db.entity.UserAmountLog;

public interface UserAmountLogRepository extends JpaRepository<UserAmountLog, Long> {
}
