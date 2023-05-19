package com.msa.mainserver.dto.response;

import lombok.*;

import javax.persistence.Column;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class FindUserResponse {

    private Long shortestEscapeTime;

    private Long longestSurvivalTime;

    private int normalMonsterKills;

    private int eliteMonsterKills;

    private int totalQuestCompleted;

    private int totalItemCrafted;

    private int deathCount;

    private int totalEscapeCount;

    private int totalPlayTime;
}
