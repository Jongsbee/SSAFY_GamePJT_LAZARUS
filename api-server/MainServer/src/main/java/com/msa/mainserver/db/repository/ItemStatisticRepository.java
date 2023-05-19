package com.msa.mainserver.db.repository;

import com.msa.mainserver.db.entity.ItemStatistic;
import io.lettuce.core.dynamic.annotation.Param;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import java.util.List;

public interface ItemStatisticRepository extends JpaRepository<ItemStatistic, Long> {

    @Query("select i.itemTotalCrafted from ItemStatistic  i where i.id in (:id) order by i.id asc")
    List<Integer> findItemTotalCraft(@Param("id")List<Long> id);

    @Query("select i.itemTotalUsed from ItemStatistic i where i.id in (:id) order by i.id asc")
    List<Integer> findItemTotalUsed(@Param("id")List<Long> id);
}
