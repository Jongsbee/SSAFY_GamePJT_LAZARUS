package com.msa.mainserver.dto.response;

import lombok.*;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class NoticeDetailResponse {
    private Long noticeId;
    private String noticeTitle;
    private String noticeType;
    private String noticeContent;
    private String noticeDate;
}
