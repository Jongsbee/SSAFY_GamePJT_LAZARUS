package com.msa.mainserver.config;

import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.CorsRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

@Configuration
public class WebConfig implements WebMvcConfigurer {

    @Override
    public void addCorsMappings(CorsRegistry registry) {
        registry.addMapping("/**")
                .allowedOrigins("*") // 허용할 오리진(도메인) 설정
                .allowedMethods("GET", "POST") // 허용할 HTTP 메소드 설정
                .allowedHeaders("*"); // 허용할 헤더 설정
    }
}
