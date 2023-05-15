package com.msa.mainserver.api.service;

import java.util.List;

import com.msa.mainserver.dto.response.FindRecordResponse;
import com.msa.mainserver.dto.response.FindUserResponse;

public interface SearchService {
    public FindUserResponse findUserActivity(String nickname);
    public List<FindRecordResponse> findUserRecord(String nickname, int page);
}
