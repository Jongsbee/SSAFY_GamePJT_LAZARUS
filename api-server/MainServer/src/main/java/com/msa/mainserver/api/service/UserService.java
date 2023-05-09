package com.msa.mainserver.api.service;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;

import com.msa.mainserver.db.entity.User;
import com.msa.mainserver.dto.request.*;
import com.msa.mainserver.dto.response.LoginResponse;

public interface UserService {

	public void userRegister(RegisterRequest request);
	public void checkDuplicateInfo(CheckDuplicateRequest request);
	public LoginResponse userLogin(LoginRequest request, HttpServletRequest httpRequest);
	public void withdrawalUser(WithdrawalUserRequest request);
	public void sendVerificationMail(String email);
	public String getVerifyEmail(String email);
	public int changeUserAmount(AmountChangeRequest request);
}
