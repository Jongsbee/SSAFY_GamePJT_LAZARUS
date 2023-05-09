package com.exodia.schedulerserver.db.entity;

import java.time.LocalDateTime;

import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import org.springframework.data.mongodb.core.mapping.Document;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Document(collation = "in_game_clear_log")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
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
