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
@Table(name = "gameplay_record")
public class GameplayRecord {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "record_id", nullable = false)
    private Long id;

    @NotNull
    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "user_id", nullable = false)
    private User user;

    @Size(max = 255)
    @NotNull
    @Column(name = "game_id", nullable = false)
    private String gameId;

    @Column(name = "game_end_time")
    private LocalDateTime gameEndTime;

    @Column(name = "kill_elite_count", nullable = false)
    private int killEliteCount;

    @Column(name = "kill_monster_count", nullable = false)
    private int killMonsterCount;

    @Column(name = "record_item_total_craft", nullable = false)
    private int recordItemTotalCraft;

    @Column(name = "escape_flag", nullable = false)
    private boolean escapeFlag;

    @Column(name = "spent_time", nullable = false)
    private long spentTime;

    @Column(name = "quest_completed_count", nullable = false)
    private int questCompletedCount;

}