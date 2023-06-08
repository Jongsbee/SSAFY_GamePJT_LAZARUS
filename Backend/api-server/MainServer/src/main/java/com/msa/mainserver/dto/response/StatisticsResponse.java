package com.msa.mainserver.dto.response;

import lombok.*;

import java.time.LocalDateTime;
import java.util.List;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class StatisticsResponse {
    List<Integer> craftItemList;
    List<Integer> useFoodList;
    List<Integer> huntedMonsterList;
    List<String> whenList;
    List<Long> spentTimeList;
}
