package com.msa.mainserver.api.controller;

import java.util.List;

import com.msa.mainserver.api.service.SearchService;
import com.msa.mainserver.dto.response.FindRecordResponse;
import com.msa.mainserver.dto.response.FindUserResponse;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequiredArgsConstructor
@Slf4j
@RequestMapping("/search")
public class SearchController {

    private final SearchService searchService;

    @GetMapping("/user")
    public ResponseEntity findUserData(@RequestParam("nickname") String nickname){

        FindUserResponse userActivity = searchService.findUserActivity(nickname);
        return ResponseEntity.ok(userActivity);
    }

    @GetMapping("/record")
    public ResponseEntity findUserRecord(@RequestParam("nickname")String nickname,@RequestParam("page")int page){
        List<FindRecordResponse> userRecord = searchService.findUserRecord(nickname, page);
        return ResponseEntity.ok(userRecord);
    }


}
