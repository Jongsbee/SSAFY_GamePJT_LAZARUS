package com.msa.mainserver.db.repository;

import com.msa.mainserver.db.entity.Notice;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;

public interface NoticeRepository extends JpaRepository<Notice, Long> {
    Page<Notice> findAllByOrderByNoticeDateDesc(Pageable pageable);
}
