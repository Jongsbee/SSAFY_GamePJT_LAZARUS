package com.exodia.schedulerserver.db.entity;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
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

	@NotNull
	@Column(name = "monster_killed", nullable = false)
	private int monsterKilled;

	public void increaseKilledCnt(){
		this.monsterKilled++;
	}

}