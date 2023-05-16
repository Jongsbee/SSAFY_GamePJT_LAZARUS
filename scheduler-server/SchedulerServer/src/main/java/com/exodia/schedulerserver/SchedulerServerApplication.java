package com.exodia.schedulerserver;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.scheduling.annotation.EnableScheduling;

@SpringBootApplication
@EnableScheduling
public class SchedulerServerApplication {

	public static void main(String[] args) {
		SpringApplication.run(SchedulerServerApplication.class, args);
	}

}
