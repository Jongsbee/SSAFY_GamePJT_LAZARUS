package com.msa.mainserver.api.service;

import com.msa.mainserver.dto.response.FindUserResponse;

public interface SearchService {
    public FindUserResponse findUserActivity(String nickname);
}
