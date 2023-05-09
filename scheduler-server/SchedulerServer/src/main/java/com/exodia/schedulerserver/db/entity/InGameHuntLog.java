package com.exodia.schedulerserver.db.entity;

import java.time.LocalDateTime;

import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import org.springframework.data.mongodb.core.mapping.Document;

import com.exodia.schedulerserver.dto.enums.CreatureType;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Document(collation = "in_game_hunt_log")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class InGameHuntLog {
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private Long id;
	private Long userId;
	private Long creatureId;
	@Enumerated(EnumType.STRING)
	private CreatureType creatureType;
	private String gameId;
	private LocalDateTime huntTime;

}
