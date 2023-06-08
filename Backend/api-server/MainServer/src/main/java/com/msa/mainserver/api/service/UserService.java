package com.msa.mainserver.api.service;

import javax.servlet.http.HttpServletRequest;

import com.msa.mainserver.dto.request.AmountChangeRequest;
import com.msa.mainserver.dto.request.CheckDuplicateRequest;
import com.msa.mainserver.dto.request.LoginRequest;
import com.msa.mainserver.dto.request.RegisterRequest;
import com.msa.mainserver.dto.request.WithdrawalUserRequest;
import com.msa.mainserver.dto.response.LoginResponse;

public interface UserService {

	void userRegister(RegisterRequest request);
	void checkDuplicateInfo(CheckDuplicateRequest request);
	LoginResponse userLogin(LoginRequest request, HttpServletRequest httpRequest);
	void withdrawalUser(WithdrawalUserRequest request);
	void sendVerificationMail(String email);
	String getVerifyEmail(String email);
	int changeUserAmount(AmountChangeRequest request);
}
