package com.msa.mainserver.db.entity;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.MapsId;
import javax.persistence.OneToOne;
import javax.persistence.Table;
import javax.validation.constraints.NotNull;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Entity
@Table(name = "monster_statistics")
public class MonsterStatistics {
	@Id
	@Column(name = "monster_id", nullable = false)
	private Long id;

	@MapsId
	@OneToOne(fetch = FetchType.LAZY, optional = false)
	@JoinColumn(name = "monster_id", nullable = false)
	private Monster monster;

	@NotNull
	@Column(name = "monster_killed", nullable = false)
	private int monsterKilled;

}