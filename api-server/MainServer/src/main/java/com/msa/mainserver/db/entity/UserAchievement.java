package com.msa.mainserver.db.entity;

import java.time.LocalDateTime;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.Table;
import javax.validation.constraints.NotNull;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import lombok.experimental.Accessors;

@Getter
@Setter
@Entity
@Table(name = "user_achievement")
@Accessors(chain = true)
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class UserAchievement {
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(name = "user_achievement_id", nullable = false)
	private Long id;

	@NotNull
	@ManyToOne(fetch = FetchType.LAZY, optional = false)
	@JoinColumn(name = "user_id", nullable = false)
	private User user;

	@NotNull
	@ManyToOne(fetch = FetchType.LAZY, optional = false)
	@JoinColumn(name = "achievement_id", nullable = false)
	private Achievement achievement;

	@NotNull
	@Column(name = "achievement_progress", nullable = false)
	private int achievementProgress;

	@NotNull
	@Column(name = "achievement_done", nullable = false)
	private boolean achievementDone;

	@Column(name = "user_achievement_date", nullable = false)
	private LocalDateTime userAchievementDate;

}