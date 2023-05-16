package com.exodia.logserver.api.controller;

import com.exodia.logserver.dto.request.*;
import org.apache.coyote.Response;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.exodia.logserver.api.service.LogService;
import com.exodia.logserver.dto.response.SuccessResponse;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@RestController
@RequiredArgsConstructor
@Slf4j
@RequestMapping("/logs")
@Tag(name="로그 저장", description = "로그 저장 관련 API입니다")
public class LogController {

	private final LogService logService;

	@Operation(summary = "아이템 제작 로그 저장", description = "어아탬 제작 로그 저장 메소드,  "
		+ "gameId : 방의 고유한 UUID 를 넣어주면 됩니다  "
		+ "userId : 아이템을 제작한 유저의 번호를 넣어주면 됩니다  "
		+ "itemId : 제작한 아이템의 번호를 넣어주면 됩니다.")
	@PostMapping("/craft")
	public ResponseEntity saveCraftLog(@RequestBody CraftLogRequest request){
		logService.saveCraftLog(request);
		return ResponseEntity.ok(new SuccessResponse("저장 완료"));
	}

	@Operation(summary = "아이템 소비 로그 저장", description = "어아탬 소비 로그 저장 메소드,  "
		+ "gameId : 방의 고유한 UUID 를 넣어주면 됩니다  "
		+ "userId : 아이템을 제작한 유저의 번호를 넣어주면 됩니다  "
		+ "itemId : 소비한  아이템의 번호를 넣어주면 됩니다.")
	@PostMapping("/use")
	public ResponseEntity saveUseLog(@RequestBody UseLogRequest useLogRequest){
		logService.saveUseLog(useLogRequest);
		return ResponseEntity.ok(new SuccessResponse("저장 완료"));
	}

	@Operation(summary = "아이템 먹방 로그 저장", description = "어아탬 먹방 로그 저장 메소드,  "
			+ "gameId : 방의 고유한 UUID 를 넣어주면 됩니다  "
			+ "userId : 아이템을 제작한 유저의 번호를 넣어주면 됩니다  "
			+ "itemId : 소비한  아이템의 번호를 넣어주면 됩니다.")
	@PostMapping("/eat")
	public ResponseEntity saveEatLog(@RequestBody EatLogRequest eatLogRequest){
		logService.saveEatLog(eatLogRequest);
		return ResponseEntity.ok(new SuccessResponse("저장 완료"));
	}

	@Operation(summary = "몬스터 사냥 로그 저장", description = "몬스터 사냥 로그 저장 메소드,  "
		+ "gameId : 방의 고유한 UUID 를 넣어주면 됩니다  "
		+ "userId : 몬스터를 사냥한 유저의 번호를 넣어주면 됩니다  "
		+ "creatureId : 사냥한 몬스터의 번호를 넣어주면 됩니다  "
		+ "creatureType : 사냥한 몬스터의 타입을 넣어주면 됩니다 ( 일반 : NORMAL, 엘리트 : ELITE )")
	@PostMapping("/hunt")
	public ResponseEntity saveHuntLog(@RequestBody HuntLogRequest request){
		logService.saveHuntLog(request);
		return ResponseEntity.ok(new SuccessResponse("저장 완료"));
	}

	@Operation(summary = "게임 클리어 로그 저장", description = "게임 클리어 로그 저장 메소드,  "
		+ "gameId : 방의 고유한 UUID 를 넣어주면 됩니다  "
		+ "userId : 게임을 클리어한 유저의 번호를 넣어주면 됩니다  "
		+ "cleared : 게임 클리어 여부를 넣어주면 됩니다 ( true : 클리어 , false : 사망 )	")
	@PostMapping("/clear")
	public ResponseEntity saveClearLog(@RequestBody ClearLogRequest request){
		logService.saveClearLog(request);
		return ResponseEntity.ok(new SuccessResponse("저장 완료"));
	}

	@Operation(summary = "퀘스트 완료 로그 저장", description = "퀘스트 완료 로그 저장 메소드,  "
		+ "gameId : 방의 고유한 UUID 를 넣어주면 됩니다  "
		+ "userId : 퀘스트를 완료한 유저의 번호를 넣어주면 됩니다  "
		+ "questId : 클리어한 퀘스트의 고유 id를 넣어주면 됩니다")
	@PostMapping("/quest")
	public ResponseEntity saveQuestLog(@RequestBody QuestLogRequest request){
		logService.saveQuestLog(request);
		return ResponseEntity.ok(new SuccessResponse("저장 완료"));
	}


}
