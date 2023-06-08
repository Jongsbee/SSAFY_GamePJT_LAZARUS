package com.exodia.schedulerserver.db.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.exodia.schedulerserver.db.entity.ItemStatistic;

public interface ItemStatisticRepository extends JpaRepository<ItemStatistic, Long> {
}