package com.exodia.schedulerserver.db.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.exodia.schedulerserver.db.entity.UserAmount;

public interface UserAmountRepository extends JpaRepository<UserAmount, Long> {
}
