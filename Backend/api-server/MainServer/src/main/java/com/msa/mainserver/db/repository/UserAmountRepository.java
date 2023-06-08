package com.msa.mainserver.db.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.msa.mainserver.db.entity.UserAmount;

public interface UserAmountRepository extends JpaRepository<UserAmount, Long> {
}
