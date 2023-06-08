package com.msa.mainserver.util;

import com.msa.mainserver.common.exception.CustomException;
import com.msa.mainserver.common.exception.CustomExceptionType;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.stereotype.Component;

import javax.mail.Message;
import javax.mail.MessagingException;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeMessage;
import java.io.UnsupportedEncodingException;

@Component
@RequiredArgsConstructor
public class EmailUtil {
    private final JavaMailSender javaMailSender;
    @Value("${spring.mail.username}")
    private String from;

    public void sendEmail(String to, String sub, String content) {
        MimeMessage message = javaMailSender.createMimeMessage();
        try {
            message.addRecipients(Message.RecipientType.TO, to);
            message.setSubject(sub);
            message.setText(content, "UTF-8", "html");
            message.setFrom(new InternetAddress(from, "MSA"));
        } catch (MessagingException e) {
            throw new CustomException(CustomExceptionType.RUNTIME_EXCEPTION);
        } catch (UnsupportedEncodingException e) {
            throw new CustomException(CustomExceptionType.RUNTIME_EXCEPTION);
        }

        javaMailSender.send(message);
    }
}