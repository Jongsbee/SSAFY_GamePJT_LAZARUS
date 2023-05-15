package com.msa.mainserver.dto.response;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.NonNull;
import lombok.Setter;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class FindRecordResponse {
	private String result;
	private int normal;
	private int elite;
	private int item;
	private int quest;
	private String gameTime;
	private String when;
}
