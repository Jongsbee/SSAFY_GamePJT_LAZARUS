package com.msa.mainserver.api.controller;

import java.util.List;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.msa.mainserver.api.service.AchievementService;
import com.msa.mainserver.dto.request.AchievementUpdateRequest;
import com.msa.mainserver.dto.response.AchievementResponse;
import com.msa.mainserver.dto.response.SuccessResponse;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@RestController
@RequiredArgsConstructor
@Slf4j
@RequestMapping("/achievements")
@Tag(name = "업적", description = "업적 관련 api 입니다.")
public class AchievementController {

	private final AchievementService achievementService;

	@GetMapping("/{userId}")
	@Operation(summary = "유저 업적 조회", description = "유저 업적 조회 메서드.")
	public ResponseEntity<List<AchievementResponse>> getAchievements(@PathVariable long userId) {
		return ResponseEntity.ok(achievementService.getAchievements(userId));
	}

	@PostMapping("/update")
	@Operation(summary = "업적 진행도 업데이트", description = "업적 진행도 업데이트 메서드.")
	public ResponseEntity<SuccessResponse> updateAchievement(@RequestBody List<AchievementUpdateRequest> requestList) {
		achievementService.updateAchievement(requestList);
		return ResponseEntity.ok(new SuccessResponse("업적 진행도 업데이트가 정상적으로 완료되었습니다."));
	}

}