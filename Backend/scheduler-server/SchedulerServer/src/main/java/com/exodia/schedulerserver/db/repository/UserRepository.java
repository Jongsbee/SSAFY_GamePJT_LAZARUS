package com.exodia.schedulerserver.db.repository;

import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;

import com.exodia.schedulerserver.db.entity.User;

public interface UserRepository extends JpaRepository<User, Long> {

	Optional<User> findByEmail(String email);
	Optional<User> findByNickname(String nickname);
}
