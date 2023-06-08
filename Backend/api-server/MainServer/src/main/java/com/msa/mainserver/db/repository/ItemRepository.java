package com.msa.mainserver.db.repository;

import com.msa.mainserver.db.entity.Item;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ItemRepository extends JpaRepository<Item, Long> {
}
