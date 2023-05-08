package com.exodia.logserver.db.entity;

import java.time.LocalDateTime;

import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import com.exodia.logserver.dto.enums.CreatureType;

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
