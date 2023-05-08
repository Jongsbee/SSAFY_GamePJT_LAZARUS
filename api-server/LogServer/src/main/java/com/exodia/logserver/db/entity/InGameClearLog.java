package com.exodia.logserver.db.entity;

import java.time.LocalDateTime;

import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

public class InGameClearLog {
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private Long id;
	private Long userId;
	private String gameId;
	private boolean isCleared;
	private LocalDateTime endTime;
	private Long spentTime;

}
