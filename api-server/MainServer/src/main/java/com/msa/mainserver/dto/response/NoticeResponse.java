package com.msa.mainserver.dto.response;

import com.msa.mainserver.dto.NoticeDto;
import lombok.*;

import java.util.List;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class NoticeResponse {

    private List<NoticeDto> notices;
    private long noticeCnt;
}
