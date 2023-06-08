package com.msa.mainserver.db.entity;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Size;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "notice")
public class Notice {
    @Id
    @Column(name = "notice_id", nullable = false)
    private Long id;

    @Size(max = 50)
    @NotNull
    @Column(name = "notice_title", nullable = false, length = 50)
    private String noticeTitle;

    @Size(max = 10)
    @NotNull
    @Column(name = "notice_type", nullable = false, length = 10)
    private String noticeType;

    @NotNull
    @Lob
    @Column(name = "notice_content", nullable = false)
    private String noticeContent;

    @Column(name = "notice_date")
    private LocalDateTime noticeDate;

}