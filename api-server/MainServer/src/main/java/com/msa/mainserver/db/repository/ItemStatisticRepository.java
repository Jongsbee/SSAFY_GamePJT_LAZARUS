package com.msa.mainserver.db.repository;

import com.msa.mainserver.db.entity.ItemStatistic;
import io.lettuce.core.dynamic.annotation.Param;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import java.util.List;

public interface ItemStatisticRepository extends JpaRepository<ItemStatistic, Long> {

    @Query("select i.itemTotalCrafted from ItemStatistic  i where i.id in (:ids) order by i.id asc")
    List<Integer> findItemTotalCraftByIdIn(@Param("ids") List<Long> ids);

    @Query("select i.itemTotalUsed from ItemStatistic i where i.id in (:ids) order by i.id asc")
    List<Integer> findItemTotalUsedByIdIn(@Param("ids") List<Long> ids);
}
