-- MariaDB dump 10.19  Distrib 10.11.3-MariaDB, for debian-linux-gnu (x86_64)
--
-- Host: localhost    Database: MSA
-- ------------------------------------------------------
-- Server version	10.11.3-MariaDB-1:10.11.3+maria~ubu2204

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `MSA`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `MSA` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;

USE `MSA`;

--
-- Table structure for table `achievements`
--

DROP TABLE IF EXISTS `achievements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `achievements` (
  `achievement_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `achievement_name` varchar(100) NOT NULL,
  `achievement_description` varchar(500) NOT NULL,
  `achievement_condition` int(11) NOT NULL,
  `achievement_reward` int(11) NOT NULL,
  PRIMARY KEY (`achievement_id`)
) ENGINE=InnoDB AUTO_INCREMENT=1001 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `achievements`
--

LOCK TABLES `achievements` WRITE;
/*!40000 ALTER TABLE `achievements` DISABLE KEYS */;
INSERT INTO `achievements` VALUES
(1,'집에 가자!','탈출 1회 성공',1,10),
(2,'전역을 명 받았습니다!','탈출 5회 성공',5,50),
(3,'집에가기싫어...','탈출 10회 성공',10,200),
(4,'불 좀 켜줄래? 내 장비좀 보게','모닥불 5회 사용',5,10),
(5,'BONFIRE LIT','모닥불 10회 사용',10,50),
(6,'장작의 왕','모닥불 50회 사용',50,100),
(7,'화살 한발로 세상을 평정해주지!','화살 10회 소비',10,10),
(8,'한조 대기중','화살 50회 소비',50,50),
(9,'바람소리가 들리시나요?','화살 100회 소비',100,100),
(10,'사슴은 총알 한발에 안죽어','탄약 5회 소비',5,10),
(11,'석양이 진다..','탄약 10회 소비',10,50),
(12,'특등사수','탄약 50회 소비',50,100),
(13,'등가교환의 법칙','조합 10회 성공',10,10),
(14,'섞고 돌리고 섞고~','조합 50회 성공',50,50),
(15,'정답이다. 연금술사!','조합 100회 성공',100,100),
(16,'나 먹방좀 할게','음식물 5회 섭취',5,10),
(17,'산해진미','음식물 10회 섭취',10,50),
(18,'이 집 음식 잘하네','음식물 50회 섭취',50,100),
(19,'오스트랄로피테쿠스','아이템 10회 사용',10,10),
(20,'호모 에렉투스','아이템 50회 사용',50,50),
(21,'호모 사피엔스','아이템 100회 사용',100,100),
(22,'앞다리살 2근만 주세요','동물 20마리 사냥',20,10),
(23,'인간이 미안해 ㅠㅠ','동물 80마리 사냥',80,50),
(24,'베어그릴스','동물 200마리 사냥',200,200),
(25,'시슴... 시슴을 조심하십시오','우두머리 5회 사냥',5,10),
(26,'일단 빛나길래 잡았습니다만..?','우두머리 20회 사냥',20,50),
(27,'이구역 우두머리는 나야','우두머리 50회 사냥',50,200),
(28,'놀라운 컨트롤!','좀비곰 1회 사냥',1,10),
(29,'우루사 과다복용','좀비곰 5회 사냥',5,50),
(30,'나혼자 네크로맨서','좀비곰 10회 사냥',10,200),
(31,'사후세계','사망 1회',1,10),
(32,'이거... 깨면 안되는건데..?','진엔딩 달성',1,50);
/*!40000 ALTER TABLE `achievements` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `gameplay_record`
--

DROP TABLE IF EXISTS `gameplay_record`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `gameplay_record` (
  `record_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user_id` bigint(20) NOT NULL,
  `game_id` varchar(255) NOT NULL,
  `game_end_time` timestamp NULL DEFAULT current_timestamp(),
  `kill_elite_count` int(11) DEFAULT 0,
  `kill_monster_count` int(11) DEFAULT 0,
  `record_item_total_craft` int(11) DEFAULT 0,
  `escape_flag` tinyint(1) DEFAULT 0,
  `spent_time` bigint(20) DEFAULT NULL,
  `quest_completed_count` int(11) DEFAULT NULL,
  PRIMARY KEY (`record_id`),
  KEY `FK_user_TO_user_record_1` (`user_id`),
  CONSTRAINT `FK_user_TO_user_record_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `gameplay_record`
--

LOCK TABLES `gameplay_record` WRITE;
/*!40000 ALTER TABLE `gameplay_record` DISABLE KEYS */;
INSERT INTO `gameplay_record` VALUES
(6,1,'testGameId1','2023-05-11 16:20:38',4,3,3,0,2593,1),
(7,2,'testGameId2','2023-05-14 15:00:24',5,100,40,0,1940,13),
(8,2,'testGameId3','2022-05-15 15:01:41',16,189,52,1,2154,4),
(9,2,'testGameId2','2023-01-15 15:00:24',5,100,40,0,1940,13),
(10,2,'testGameId3','2020-05-15 15:01:41',16,189,52,1,2154,4),
(11,2,'testGameId2','2023-05-15 13:00:24',5,100,40,0,1940,13),
(12,2,'testGameId3','2022-04-15 15:01:41',16,189,52,0,2154,4),
(13,2,'testGameId2','2023-01-15 15:00:24',5,100,40,1,1940,13),
(14,2,'testGameId3','2022-05-15 15:01:41',16,189,52,0,2154,4),
(15,2,'testGameId3','2022-05-15 15:01:41',16,189,52,1,2154,4),
(16,2,'testGameId2','2023-01-15 15:00:24',5,100,40,0,1940,13),
(17,2,'testGameId3','2022-05-15 15:01:41',16,189,52,0,2154,4),
(18,2,'testGameId2','2023-01-15 15:00:24',5,100,40,1,1940,13),
(19,2,'testGameId3','2022-05-15 15:01:41',16,189,52,0,2154,4),
(21,10,'game_id_1','2023-05-18 10:06:57',2,13,21,0,314,5),
(22,2,'testGameId3','2023-05-18 17:01:47',16,189,52,1,2154,4);
/*!40000 ALTER TABLE `gameplay_record` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `item`
--

DROP TABLE IF EXISTS `item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `item` (
  `item_id` bigint(20) NOT NULL,
  `item_name` varchar(50) NOT NULL,
  `item_type` varchar(20) DEFAULT NULL,
  `item_eatable` tinyint(1) NOT NULL DEFAULT 0 COMMENT '소비 아이템 중 먹는 아이템이라면 true',
  PRIMARY KEY (`item_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `item`
--

LOCK TABLES `item` WRITE;
/*!40000 ALTER TABLE `item` DISABLE KEYS */;
INSERT INTO `item` VALUES
(0,'WOOD_LV1','STUFF',0),
(1,'WOOD_LV2','STUFF',0),
(10,'STONE_LV1','STUFF',0),
(11,'STONE_LV2','STUFF',0),
(30,'MUSHROOM_1','USABLE',1),
(31,'MUSHROOM_2','USABLE',1),
(32,'MUSHROOM_3','USABLE',1),
(40,'MEAT_LV1','USABLE',1),
(41,'MEAT_LV2','USABLE',1),
(42,'MEAT_LV3','USABLE',1),
(60,'CAMPFIRE','USABLE',0),
(61,'ARROW','EQUIPMENT',0),
(62,'MAGAZINE','EQUIPMENT',0),
(70,'CRYSTAL_1','STUFF',0),
(71,'CRYSTAL_2','STUFF',0),
(72,'CRYSTAL_3','STUFF',0),
(100,'AXE_1','EQUIPMENT',0),
(101,'AXE_2','EQUIPMENT',0),
(110,'PICKAXE_1','EQUIPMENT',0),
(111,'PICKAXE_2','EQUIPMENT',0),
(120,'BOW_1','EQUIPMENT',0),
(121,'BOW_2','EQUIPMENT',0),
(200,'BBQ_MEAT_LV1','USABLE',1),
(201,'BBQ_MEAT_LV2','USABLE',1),
(202,'BBQ_MEAT_LV3','USABLE',1),
(210,'SOUP_MEAT_LV1','USABLE',1),
(211,'SOUP_MEAT_LV2','USABLE',1),
(212,'SOUP_MEAT_LV3','USABLE',1);
/*!40000 ALTER TABLE `item` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `item_statistics`
--

DROP TABLE IF EXISTS `item_statistics`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `item_statistics` (
  `item_id` bigint(20) NOT NULL,
  `item_total_used` int(11) NOT NULL DEFAULT 0,
  `item_total_crafted` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`item_id`),
  CONSTRAINT `FK_item_TO_item_statistics_1` FOREIGN KEY (`item_id`) REFERENCES `item` (`item_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `item_statistics`
--

LOCK TABLES `item_statistics` WRITE;
/*!40000 ALTER TABLE `item_statistics` DISABLE KEYS */;
INSERT INTO `item_statistics` VALUES
(1,0,0),
(11,0,0),
(30,0,0),
(31,0,0),
(32,0,0),
(40,2,0),
(41,1,0),
(42,0,0),
(60,8,0),
(61,8,22),
(100,0,2),
(101,0,5),
(110,0,0),
(111,0,6),
(120,0,0),
(121,0,10),
(200,0,0),
(201,8,0),
(202,0,0),
(210,0,0),
(211,0,0),
(212,0,0);
/*!40000 ALTER TABLE `item_statistics` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `item_storage_log`
--

DROP TABLE IF EXISTS `item_storage_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `item_storage_log` (
  `item_storage_log_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user_id` bigint(20) NOT NULL,
  `item_id` bigint(20) NOT NULL,
  `item_storage_log_type` tinyint(4) DEFAULT NULL COMMENT '0 : 획득 , 1 : 소비',
  PRIMARY KEY (`item_storage_log_id`),
  KEY `FK_user_TO_item_storage_log_1` (`user_id`),
  KEY `FK_item_TO_item_storage_log_1` (`item_id`),
  CONSTRAINT `FK_item_TO_item_storage_log_1` FOREIGN KEY (`item_id`) REFERENCES `item` (`item_id`),
  CONSTRAINT `FK_user_TO_item_storage_log_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `item_storage_log`
--

LOCK TABLES `item_storage_log` WRITE;
/*!40000 ALTER TABLE `item_storage_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `item_storage_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `monster`
--

DROP TABLE IF EXISTS `monster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `monster` (
  `monster_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `monster_name` varchar(50) DEFAULT NULL,
  `monster_boss` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`monster_id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `monster`
--

LOCK TABLES `monster` WRITE;
/*!40000 ALTER TABLE `monster` DISABLE KEYS */;
INSERT INTO `monster` VALUES
(1,'암사슴',0),
(2,'보스암사슴',1),
(3,'숫사슴',0),
(4,'보스숫사슴',1),
(5,'멧돼지',0),
(6,'늑대',0),
(7,'보스늑대',1),
(8,'곰',0),
(9,'보스곰',1),
(10,'좀비숫사슴',0),
(11,'좀비늑대',0),
(12,'좀비보스곰',1),
(13,'좀비무스',0);
/*!40000 ALTER TABLE `monster` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `monster_statistics`
--

DROP TABLE IF EXISTS `monster_statistics`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `monster_statistics` (
  `monster_id` bigint(20) NOT NULL,
  `monster_killed` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`monster_id`),
  CONSTRAINT `FK_monster_TO_monster_statistics_1` FOREIGN KEY (`monster_id`) REFERENCES `monster` (`monster_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `monster_statistics`
--

LOCK TABLES `monster_statistics` WRITE;
/*!40000 ALTER TABLE `monster_statistics` DISABLE KEYS */;
INSERT INTO `monster_statistics` VALUES
(1,6),
(2,0),
(3,6),
(4,0),
(5,14),
(6,0),
(7,0),
(8,0),
(9,4),
(10,0),
(11,0),
(12,0),
(13,0);
/*!40000 ALTER TABLE `monster_statistics` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notice`
--

DROP TABLE IF EXISTS `notice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `notice` (
  `notice_id` bigint(20) NOT NULL,
  `notice_title` varchar(50) NOT NULL,
  `notice_type` varchar(10) NOT NULL,
  `notice_content` longtext NOT NULL,
  `notice_date` timestamp NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`notice_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notice`
--

LOCK TABLES `notice` WRITE;
/*!40000 ALTER TABLE `notice` DISABLE KEYS */;
INSERT INTO `notice` VALUES
(1,'0.5.0 LAZARUS 오픈베타 실시합니다','공지','■ LAZARUS 오픈베타 시작!\n\n■ 판타지풍 생존게임 LAZARUS 입니다!\n\n■ 즐겁게 플레이해주세요!','2023-05-19 00:43:17');
/*!40000 ALTER TABLE `notice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `storage`
--

DROP TABLE IF EXISTS `storage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `storage` (
  `storage_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user_id` bigint(20) NOT NULL,
  `item_id` bigint(20) NOT NULL,
  `auction_reg` tinyint(1) DEFAULT 0,
  PRIMARY KEY (`storage_id`),
  KEY `FK_user_TO_storage_1` (`user_id`),
  KEY `FK_item_TO_storage_1` (`item_id`),
  CONSTRAINT `FK_item_TO_storage_1` FOREIGN KEY (`item_id`) REFERENCES `item` (`item_id`),
  CONSTRAINT `FK_user_TO_storage_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `storage`
--

LOCK TABLES `storage` WRITE;
/*!40000 ALTER TABLE `storage` DISABLE KEYS */;
/*!40000 ALTER TABLE `storage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `user_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `email` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  `nickname` varchar(10) NOT NULL,
  `reg_date` timestamp NOT NULL DEFAULT current_timestamp(),
  `user_active` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `UC_USER` (`email`,`nickname`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES
(1,'test@ruu.kr','$2a$10$0Ec4pIueIIPeaf6CPyftS.aKbTsOBPhVmbXOYz1IUy03MPFcvR35i','tester','2023-05-11 10:53:22',1),
(2,'devjunmo@ruu.kr','$2a$10$8AYw3VNfpuwXJcAJQl5wW.tsxcRDjiQDBC788rMSTp/jrxJpDFQeK','포톤갓정해석','2023-05-12 14:15:28',1),
(8,'gregori@ruu.kr','$2a$10$Iz76CosdwhTPads9fOkREOZ0hr4zfDmki8evS2E5qPEhnUbdbkk.6','성스러운그레고리','2023-05-16 03:55:57',1),
(9,'junmo@ruu.kr','$2a$10$tcZRY.jzwm/XUJ/b/wL6eu99BCRuWC8hPEsljKFjfmwDn62pHRpcC','옹심이아빠','2023-05-16 12:21:47',1),
(10,'upjunmo@ruu.kr','$2a$10$RPRZPAMqAAK2IhZPsROTMOIhDfAKejLPx2GIKzAdYsGreWzpg1um6','넝담곰','2023-05-16 12:28:40',1),
(12,'leftjunmo@ruu.kr','$2a$10$.Ibi.4YMOh.0jvSgKc0OGOFcquSIro39F.K3vWYN0tBK2k4CfOKn6','좌준모','2023-05-16 12:31:55',0),
(13,'rightjunmo@ruu.kr','$2a$10$kwhao9MRtpbJUxzUQKBHA.TmGotdxnh7nRTrUZ.QRKcyFqb4QaUM2','우준모','2023-05-16 12:48:02',0),
(27,'najunmo@ruu.kr','$2a$10$ebvCFWDm15WshE6nlGHfEemBr9SwfCFSvaFba2n5cDN/h6HVdUC2y','나준모','2023-05-19 08:37:31',1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_achievement`
--

DROP TABLE IF EXISTS `user_achievement`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_achievement` (
  `user_achievement_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user_id` bigint(20) NOT NULL,
  `achievement_id` bigint(20) NOT NULL,
  `achievement_progress` int(11) NOT NULL,
  `achievement_done` tinyint(1) NOT NULL DEFAULT 0,
  `user_achievement_date` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`user_achievement_id`),
  KEY `FK_user_TO_user_achievement_1` (`user_id`),
  KEY `FK_achievements_TO_user_achievement_1` (`achievement_id`),
  CONSTRAINT `FK_achievements_TO_user_achievement_1` FOREIGN KEY (`achievement_id`) REFERENCES `achievements` (`achievement_id`),
  CONSTRAINT `FK_user_TO_user_achievement_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_achievement`
--

LOCK TABLES `user_achievement` WRITE;
/*!40000 ALTER TABLE `user_achievement` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_achievement` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_activity`
--

DROP TABLE IF EXISTS `user_activity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_activity` (
  `user_id` bigint(20) NOT NULL,
  `recent_login_time` timestamp NULL DEFAULT current_timestamp(),
  `recent_login_ip` varchar(20) DEFAULT NULL,
  `shortest_escape_time` bigint(20) DEFAULT NULL,
  `longest_survival_time` bigint(20) DEFAULT NULL,
  `normal_monster_kills` int(11) DEFAULT NULL,
  `elite_monster_kills` varchar(255) DEFAULT NULL,
  `death_count` int(11) DEFAULT NULL,
  `total_quest_completed` int(11) DEFAULT NULL,
  `total_item_crafted` int(11) DEFAULT NULL,
  `total_escape_count` int(11) DEFAULT NULL,
  `total_play_time` int(11) DEFAULT NULL,
  `total_item_used` int(11) DEFAULT NULL,
  `total_item_eaten` int(11) DEFAULT NULL,
  PRIMARY KEY (`user_id`),
  CONSTRAINT `FK_user_TO_user_activity_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_activity`
--

LOCK TABLES `user_activity` WRITE;
/*!40000 ALTER TABLE `user_activity` DISABLE KEYS */;
INSERT INTO `user_activity` VALUES
(1,NULL,NULL,21354,21354,3,'4',1,1,3,0,21354,13,23),
(2,'2023-05-12 14:33:14','0:0:0:0:0:0:0:1',1910,2584,155,'22',6,28,89,13,15940,16,12),
(8,NULL,NULL,2663,2794,42,'21',4,15,67,11,14390,1,1),
(9,'2023-05-16 13:45:41','172.26.12.139',2184,2533,25,'3',2,14,25,5,9059,12,8),
(10,NULL,NULL,314,314,26,'4',2,10,42,0,628,17,9),
(12,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(13,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(15,'2023-05-18 23:10:29','172.26.12.139',NULL,NULL,0,'0',0,0,0,0,0,0,0),
(16,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(27,'2023-05-19 08:39:48','172.26.12.139',NULL,NULL,0,'0',0,0,0,0,0,0,0);
/*!40000 ALTER TABLE `user_activity` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_amount`
--

DROP TABLE IF EXISTS `user_amount`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_amount` (
  `user_id` bigint(20) NOT NULL,
  `user_amount` int(11) DEFAULT NULL,
  PRIMARY KEY (`user_id`),
  CONSTRAINT `FK_user_TO_user_amount_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_amount`
--

LOCK TABLES `user_amount` WRITE;
/*!40000 ALTER TABLE `user_amount` DISABLE KEYS */;
INSERT INTO `user_amount` VALUES
(1,5000),
(2,0),
(8,0),
(9,0),
(10,0),
(12,0),
(13,0),
(15,0),
(16,0),
(27,0);
/*!40000 ALTER TABLE `user_amount` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_amount_log`
--

DROP TABLE IF EXISTS `user_amount_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_amount_log` (
  `user_cache_log_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user_id` bigint(20) NOT NULL,
  `amount_change` int(11) DEFAULT NULL,
  `total_amount` int(11) DEFAULT NULL,
  PRIMARY KEY (`user_cache_log_id`),
  KEY `FK_user_TO_user_amount_log_1` (`user_id`),
  CONSTRAINT `FK_user_TO_user_amount_log_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_amount_log`
--

LOCK TABLES `user_amount_log` WRITE;
/*!40000 ALTER TABLE `user_amount_log` DISABLE KEYS */;
INSERT INTO `user_amount_log` VALUES
(1,1,5000,5000),
(2,1,5000,10000),
(3,1,-5000,5000);
/*!40000 ALTER TABLE `user_amount_log` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-05-19  0:46:24
