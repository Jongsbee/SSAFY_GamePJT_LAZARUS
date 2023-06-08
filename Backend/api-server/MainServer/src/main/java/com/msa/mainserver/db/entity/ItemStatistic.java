package com.msa.mainserver.db.entity;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import javax.validation.constraints.NotNull;

@Getter
@Setter
@Entity
@Table(name = "item_statistics")
public class ItemStatistic {
    @Id
    @Column(name = "item_id", nullable = false)
    private Long id;

    @MapsId
    @OneToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "item_id", nullable = false)
    private Item item;

    @NotNull
    @Column(name = "item_total_used", nullable = false)
    private int itemTotalUsed;

    @Column(name = "item_total_crafted", nullable = false)
    private int itemTotalCrafted;

}