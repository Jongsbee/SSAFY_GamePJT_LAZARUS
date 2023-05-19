package com.msa.mainserver.dto;

import io.swagger.v3.oas.annotations.callbacks.Callback;
import lombok.*;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class NoticeDto {
    private Long noticeId;
    private String noticeTitle;
    private String noticeType;
    private String noticeDate;
}
