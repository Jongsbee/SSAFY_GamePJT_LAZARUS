package com.exodia.schedulerserver.db.entity;

import java.time.LocalDate;
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

@Getter
@Setter
@Entity
@Table(name = "user_record")
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class UserRecord {
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(name = "record_id", nullable = false)
	private Long id;

	@NotNull
	@ManyToOne(fetch = FetchType.LAZY, optional = false)
	@JoinColumn(name = "user_id", nullable = false)
	private User user;

	@Column(name = "game_id", nullable = false)
	private String gameId;

	@Column(name = "game_end_time")
	private LocalDateTime gameEndTime;

	@Column(name = "kill_elite_count")
	private int killEliteCount;

	@Column(name = "kill_monster_count")
	private int killMonsterCount;

	@Column(name = "record_item_total_craft")
	private int itemTotalCraft;

	@Column(name = "escape_flag")
	private boolean userEscape;

	@Column(name = "spent_time")
	private Long spentTime;

	@Column(name = "quest_completed_count")
	private int totalQuestClearCnt;
}
