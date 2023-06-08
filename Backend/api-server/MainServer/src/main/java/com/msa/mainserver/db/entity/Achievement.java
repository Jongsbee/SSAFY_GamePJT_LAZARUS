package com.msa.mainserver.db.entity;

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
@Table(name = "achievements")
public class Achievement {
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(name = "achievement_id", nullable = false)
	private Long id;

	@Size(max = 100)
	@NotNull
	@Column(name = "achievement_name", nullable = false, length = 100)
	private String achievementName;

	@Size(max = 500)
	@NotNull
	@Column(name = "achievement_description", nullable = false, length = 500)
	private String achievementDescription;

	@NotNull
	@Column(name = "achievement_condition", nullable = false)
	private int achievementCondition;

	@NotNull
	@Column(name = "achievement_reward", nullable = false)
	private Integer achievementReward;

}