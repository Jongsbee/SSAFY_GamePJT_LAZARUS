package com.msa.mainserver.dto.response;

import java.util.List;

import com.msa.mainserver.dto.CraftRankDto;
import com.msa.mainserver.dto.EscapeRankDto;
import com.msa.mainserver.dto.HuntRankDto;
import com.msa.mainserver.dto.QuestRankDto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.RequiredArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class RankingResponse {
	List<EscapeRankDto> escapeRanks;
	List<HuntRankDto> huntRanks;
	List<CraftRankDto> craftRanks;
	List<QuestRankDto> questRanks;

}


