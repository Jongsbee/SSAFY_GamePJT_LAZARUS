package com.exodia.logserver.db.entity;

import java.math.BigInteger;
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

@Document(collection = "in_game_quest_log")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class InGameQuestLog {
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private BigInteger id;
	private Long userId;
	private Long questId;
	private String gameId;
	private LocalDateTime clearTime;
}
