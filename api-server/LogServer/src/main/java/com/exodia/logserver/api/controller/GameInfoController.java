package com.exodia.logserver.api.controller;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import com.exodia.logserver.api.service.GameInfoService;
import com.exodia.logserver.dto.Request.CreateRoomRequest;
import com.exodia.logserver.dto.response.CreateRoomResponse;

import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@RestController
@RequiredArgsConstructor
@Slf4j
@RequestMapping("/games")
@Tag(name="게임정보", description = "게임 정보 관련 api 입니다")
public class GameInfoController {
	private final GameInfoService gameInfoService;

	@PostMapping("/create/room")
	public ResponseEntity createRoom(@RequestBody CreateRoomRequest request){
		CreateRoomResponse response = gameInfoService.createRoom(request);
		return ResponseEntity.ok(response);

	}

}
