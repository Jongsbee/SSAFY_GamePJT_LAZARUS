package com.msa.mainserver.db.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.msa.mainserver.db.entity.UserAmount;
import com.msa.mainserver.db.entity.UserAmountLog;

public interface UserAmountLogRepository extends JpaRepository<UserAmountLog, Long> {
}
