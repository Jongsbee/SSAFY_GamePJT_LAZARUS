package com.exodia.logserver.api.controller;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import com.exodia.logserver.api.service.GameInfoService;
import com.exodia.logserver.dto.request.StartRoomRequest;

import io.swagger.v3.oas.annotations.Operation;
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

	/**
	 * 게임을 시작하기 위한 메소드
	 * @param request => gameId : 방의 고유한 UUID 같은 것을 넣으면 된다
	 *                   users : 방에 참가해있는 유저들의 번호를 LIST 형태로 담아서 넘겨주면 된다 (ArrayList)
	 * @return OK
	 */
	@PostMapping("/start")
	@Operation(summary = "게임 시작", description = "게임 시작 메소드, "
		+ "gameId : 방의 고유한 UUID 를 넣어주면 됩니다"
		+ "users : 방에 참가해있는 유저들의 번호를 List형태로 넘겨주시면 됩니다.")
	public ResponseEntity startGame(@RequestBody StartRoomRequest request) {
		gameInfoService.startGame(request);
		return ResponseEntity.ok("게임 시작 성공");
	}

}
