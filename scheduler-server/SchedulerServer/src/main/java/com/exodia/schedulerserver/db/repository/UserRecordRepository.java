package com.exodia.schedulerserver.db.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.exodia.schedulerserver.db.entity.UserRecord;

public interface UserRecordRepository extends JpaRepository<UserRecord, Long> {
}
