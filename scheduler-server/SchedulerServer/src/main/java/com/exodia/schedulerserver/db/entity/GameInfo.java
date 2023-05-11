package com.exodia.schedulerserver.db.entity;

import java.time.LocalDateTime;
import java.util.ArrayList;

import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.Id;

import org.springframework.data.mongodb.core.mapping.Document;

import com.exodia.schedulerserver.dto.enums.GameStatus;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Document(collection = "game_info")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class GameInfo {
	@Id
	private String id;
	private LocalDateTime startTime;
	private LocalDateTime endTime;
	@Enumerated(EnumType.STRING)
	private GameStatus gameStatus;
	private ArrayList<Long> users;
}
