package com.exodia.schedulerserver.db.entity;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Size;

import com.exodia.schedulerserver.dto.enums.ItemType;

import lombok.Getter;
import lombok.Setter;

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
	@Column(name = "item_type", length = 20)
	@Enumerated(EnumType.STRING)
	private ItemType itemType;

    @NotNull
    @Column(name = "item_eatable", nullable = false)
    private boolean itemEatable;

}