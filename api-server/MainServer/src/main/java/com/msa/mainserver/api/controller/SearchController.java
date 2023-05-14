package com.msa.mainserver.api.controller;

import com.msa.mainserver.api.service.SearchService;
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
    public ResponseEntity findUserDate(@RequestParam("nickname") String nickname){

        FindUserResponse userActivity = searchService.findUserActivity(nickname);
        return ResponseEntity.ok(userActivity);
    }


}
