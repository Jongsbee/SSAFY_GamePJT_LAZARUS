package com.exodia.schedulerserver.db.entity;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Size;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Entity
@Table(name = "monster")
public class Monster {
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(name = "monster_id", nullable = false)
	private Long id;

	@Size(max = 50)
	@Column(name = "monster_name", length = 50)
	private String monsterName;

	@NotNull
	@Column(name = "monster_boss", nullable = false)
	private boolean monsterBoss;

}