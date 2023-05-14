package com.msa.mainserver.db.entity;

import com.msa.mainserver.dto.enums.ItemType;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Size;

@Getter
@Setter
@Entity
@Table(name = "item")
public class Item {
    @Id
    @Column(name = "item_id", nullable = false)
    private Long id;

    @Size(max = 50)
    @NotNull
    @Column(name = "item_name", nullable = false, length = 50)
    private String itemName;

    @Size(max = 20)
    @Enumerated(EnumType.STRING)
    @Column(name = "item_type", length = 20)
    private ItemType itemType;

}