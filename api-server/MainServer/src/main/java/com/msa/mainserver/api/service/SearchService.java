package com.msa.mainserver.api.service;

import java.util.List;

import com.msa.mainserver.dto.response.*;

public interface SearchService {
    public FindUserResponse findUserActivity(String nickname);
    public List<FindRecordResponse> findUserRecord(String nickname, int page);
    public RankingResponse getRanking();
    public StatisticsResponse getStatistics();
    public NoticeResponse getNotice(int page);
    public NoticeDetailResponse getNoticeDetail(Long id);
}
