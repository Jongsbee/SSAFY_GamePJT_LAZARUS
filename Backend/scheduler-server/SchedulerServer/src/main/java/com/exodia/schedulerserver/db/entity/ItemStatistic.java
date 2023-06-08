package com.exodia.schedulerserver.db.entity;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.MapsId;
import javax.persistence.OneToOne;
import javax.persistence.Table;
import javax.validation.constraints.NotNull;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@Entity
@NoArgsConstructor
@AllArgsConstructor
@Builder
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

	public void increaseTotalCraft(){
		this.itemTotalCrafted++;
	}

	public void increaseTotalUse(){
		this.itemTotalUsed++;
	}

}