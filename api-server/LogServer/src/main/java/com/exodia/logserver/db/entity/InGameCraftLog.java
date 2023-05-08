package com.exodia.logserver.db.entity;

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

@Document(collation = "in_game_craft_log")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class InGameCraftLog {
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private Long id;
	private Long userId;
	private Long itemId;
	private String gameId;
	private LocalDateTime createTime;

}
