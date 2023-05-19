package com.msa.mainserver.util;

import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Component;

@Component
public class BcryptUtil {
	private static final BCryptPasswordEncoder encoder = new BCryptPasswordEncoder();

	public static String encryptPassword(String password) {
		return encoder.encode(password);
	}

	public static boolean checkPassword(String password, String encryptedPassword) {
		return encoder.matches(password, encryptedPassword);
	}
}
