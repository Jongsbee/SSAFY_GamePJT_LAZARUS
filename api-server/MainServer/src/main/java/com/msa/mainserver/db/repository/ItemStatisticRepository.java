package com.msa.mainserver.db.repository;

import com.msa.mainserver.db.entity.ItemStatistic;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ItemStatisticRepository extends JpaRepository<ItemStatistic, Long> {
}
