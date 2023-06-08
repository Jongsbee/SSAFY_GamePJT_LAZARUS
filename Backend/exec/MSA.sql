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
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
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
(22,2,'testGameId3','2023-05-18 17:01:47',16,189,52,1,2154,4),
(23,15,'a13e7ec7-4c38-4bf1-a2b1-8233dd6c2a69','2023-05-19 14:28:18',0,3,1,0,283,1),
(24,33,'e80d1ce4-f2a1-42aa-a3e3-a4ebd0756485','2023-05-19 17:03:32',0,2,0,0,530,2),
(25,35,'9ea4d4e2-21d0-41ac-aa83-efdd323af0c1','2023-05-19 17:04:42',0,0,0,0,321,0),
(26,37,'179ce1f7-c832-4996-bac2-34d8c3bccea3','2023-05-19 19:08:07',0,0,0,0,99,0),
(27,38,'38996b12-fdfc-4a6f-a985-ee3b8c009db7','2023-05-19 19:28:00',0,0,0,0,114,0),
(28,38,'34eef73e-cead-4fab-af5b-0251a511cead','2023-05-19 19:41:25',0,0,0,0,403,0),
(29,39,'cb6346e4-dabb-4389-a78b-adfa4acfe47a','2023-05-21 00:46:33',0,0,0,0,2453,0),
(30,37,'0e86270a-2e26-4e18-bf36-37bdade41dfd','2023-05-22 12:38:25',0,2,0,0,235254,2),
(31,37,'5896b281-8a36-4170-b491-be29c94e389b','2023-05-22 13:03:15',3,9,0,0,1386,6),
(32,37,'1629c41c-68d6-4e1b-9263-9251aa3a7d47','2023-05-22 14:12:28',0,1,0,0,314,2),
(33,37,'b8cade6b-72a6-4b38-81a7-7c8da0bf9918','2023-05-22 14:42:50',2,13,28,0,1749,2),
(34,37,'d9a1e988-feab-4f2f-95c6-5e456ae7d8bb','2023-05-22 15:07:19',2,3,8,0,1329,3),
(35,15,'e1380a66-6e83-46b0-ada2-860792d18fec','2023-05-22 16:42:46',0,0,2,0,96,1),
(36,15,'fd8afdbf-5ce7-4be7-a14d-0d1e51789e93','2023-05-22 17:33:40',1,4,0,0,379,2),
(37,37,'7b6aa4a4-eb40-49b3-bbe1-4de1feccf7d6','2023-05-23 09:23:01',8,23,35,0,65399,6),
(38,15,'64a15baf-fc11-441f-bedc-1c5e9a877b68','2023-05-24 12:47:08',1,5,5,0,619,4),
(39,50,'347accb5-924b-4bd2-aa82-d56719b87695','2023-05-24 17:52:19',1,3,0,0,203,0);
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
(1,0,1),
(11,0,0),
(30,0,0),
(31,0,0),
(32,0,0),
(40,3,0),
(41,7,0),
(42,13,0),
(60,15,7),
(61,8,22),
(100,0,2),
(101,0,5),
(110,0,0),
(111,0,6),
(120,0,0),
(121,0,10),
(200,19,19),
(201,22,19),
(202,30,33),
(210,3,0),
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
(3,24),
(4,6),
(5,26),
(6,14),
(7,3),
(8,18),
(9,13),
(10,3),
(11,3),
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
(1,'v0.5.0 LAZARUS 오픈베타 실시합니다','공지','■ LAZARUS 오픈베타 시작!\n\n■ 판타지풍 생존게임 LAZARUS 입니다!\n\n■ 즐겁게 플레이해주세요!','2023-05-19 00:43:17'),
(2,'v2.5.0 LAZARUS 업데이트 내역','공지','■ 로그인화면에서 tab키를 사용할 수 있게 해두었습니다.\n\n■ 인게임 화면에 레이더를 추가하였습니다.\n    □동물 : 초록색\n    □좀비몬스터 : 빨간색\n    □일지 : 일지아이콘\n    □건물 : 파란색 삼각형','2023-05-24 04:49:55');
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
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES
(1,'test@gmail.com','$2a$10$0Ec4pIueIIPeaf6CPyftS.aKbTsOBPhVmbXOYz1IUy03MPFcvR35i','tester','2023-05-11 10:53:22',1),
(2,'devjunmo@ruu.kr','$2a$10$8AYw3VNfpuwXJcAJQl5wW.tsxcRDjiQDBC788rMSTp/jrxJpDFQeK','포톤갓정해석','2023-05-12 14:15:28',1),
(8,'0001','$2a$10$Iz76CosdwhTPads9fOkREOZ0hr4zfDmki8evS2E5qPEhnUbdbkk.6','성스러운그레고리','2023-05-16 03:55:57',1),
(9,'junmo@ruu.kr','$2a$10$tcZRY.jzwm/XUJ/b/wL6eu99BCRuWC8hPEsljKFjfmwDn62pHRpcC','옹심이아빠','2023-05-16 12:21:47',1),
(10,'upjunmo@ruu.kr','$2a$10$RPRZPAMqAAK2IhZPsROTMOIhDfAKejLPx2GIKzAdYsGreWzpg1um6','넝담곰','2023-05-16 12:28:40',1),
(12,'leftjunmo@ruu.kr','$2a$10$.Ibi.4YMOh.0jvSgKc0OGOFcquSIro39F.K3vWYN0tBK2k4CfOKn6','좌준모','2023-05-16 12:31:55',0),
(13,'rightjunmo@ruu.kr','$2a$10$kwhao9MRtpbJUxzUQKBHA.TmGotdxnh7nRTrUZ.QRKcyFqb4QaUM2','우준모','2023-05-16 12:48:02',0),
(15,'0002','$2a$10$enHscp.4Ko5BX0ToOgKxEumTHbZh.GYgnwpHO4gt52kHJNJLG09Qa','종스비','2023-05-18 16:35:11',1),
(16,'0003','$2a$10$guKIvCkJUhXmKHEqAY7O0uvdxSV20knw9.wa8oslcjZtvSrtW8se6','이도옹','2023-05-19 02:10:01',0),
(27,'najunmo@ruu.kr','$2a$10$ebvCFWDm15WshE6nlGHfEemBr9SwfCFSvaFba2n5cDN/h6HVdUC2y','나준모','2023-05-19 08:37:31',1),
(28,'0004','$2a$10$KRszmSix.8bXsF0QDkJgUusIb29dRXAvcfJp2MwUkvWS1kFRoQc96','장푸니','2023-05-19 16:20:51',0),
(29,'0005','$2a$10$HMIO1fkUo7YeGyGi.RUs8uuooK2mEvc8Ee6miu44sGmxNjrgURzn2','Young','2023-05-19 16:22:07',0),
(33,'0006','$2a$10$CHzWSZdmUyYIj6ntjhJ5VuKkJNH/Bt2Uej78JrR7JSwo3q8c23J6G','장푼이','2023-05-19 16:53:38',1),
(34,'0007','$2a$10$9YPtnxN5InZJnlTjt/CeXuB7FGN0wC3c0f3rzFQAp3kU.bZKl7i0y','Gittgi','2023-05-19 16:55:21',1),
(35,'0008','$2a$10$5Tycy7qkPjSUYBQb7xkT1.r0krnNDaBJl0ASCUbxdVKVHLn7AZDRC','위험한남자김냐옹','2023-05-19 16:57:35',1),
(36,'asdasdasdasd@asd.com','$2a$10$si6uXz.3jOe87OapyuRPLOojXBJrIFRTEEqeb7E0BjtNM/CvGyjgu','Asdasdasd','2023-05-19 17:27:51',0),
(37,'0009','$2a$10$jVIy1Z5Bc6mKu5TBjgdBC.z9Fw2ZapHqcpTOQ6TaJoQtdqiGo1jsC','쑤','2023-05-19 18:49:13',1),
(38,'0010','$2a$10$Rsc9kAtKPGB9nkdYFgn/r.pZAmwzHHXxDWiveszvZ/ogO5DDnfdW2','Riri','2023-05-19 19:22:01',1),
(39,'0011','$2a$10$c3X6ysqNP/bHp8I4Q2GhH.M5GU5v0MMqvmTp6DpV7k77IuyGwQ2O.','강컨','2023-05-21 00:00:19',1),
(40,'0012','$2a$10$m3NeMeYucxb4gq/eUjzqP.bG/lCdH3DNOvU5v1b1yRExkI/h6pcAC','종스비비','2023-05-22 12:47:31',0),
(41,'0013','$2a$10$4UgGtwFLTQxwp..dHF0VvOsuMNxMqOrDgxC0ut9vtnhbVK9Yrd9aK','존예존예','2023-05-22 13:29:47',1),
(42,'asdafasf@naver.com','$2a$10$rcferjtyVlYHbwxgKGXi.ez8.iYR9Lh3ax0m1JY4XEkrnjPM8DjsG','킹갓','2023-05-23 14:38:32',0),
(43,'asdafasf432@naver.com','$2a$10$Hje26gvpUgSbDE7LRSKwI.MY6Ka1KiKp9pppFXmKnM1nBrlDRnEXq','킹가앗','2023-05-23 14:38:58',0),
(44,'asdq12awe@naver.com','$2a$10$/FHE.MuQwa7qpL/F5Q0FsO/zDD5ITGUCEKGQ65Kj8qlqYYZWfePXm','Zzzdasdas','2023-05-23 17:19:05',0),
(45,'0014','$2a$10$JVrLmATD0uDYNPsTOaukbekp4lSQrz0gLoqgravQ.COOaX5nrtEya','Kinggodd','2023-05-23 17:20:18',1),
(46,'duel@naver.com','$2a$10$zMOSh7eT4/CHW4vfZkXqw.xpWV7SfYSEdFKpXtEVJzuVhb0ma651y','듀얼마스터','2023-05-24 17:46:11',0),
(49,'triple@naver.com','$2a$10$4UMB07jTt7esJ7wz/xAFR.RCwOsxKjFi/FcLAto4REyWD1P4fuwuC','트리플마스터','2023-05-24 17:46:58',0),
(50,'0015','$2a$10$StobZB/PtjHzrk1JH0w.Eec5ejqj4ub9U45ehsepo.Xlm7FrnZRJm','Triple','2023-05-24 17:47:52',1);
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
(1,NULL,NULL,NULL,NULL,3,'4',1,1,3,0,21354,13,23),
(2,'2023-05-12 14:33:14','0:0:0:0:0:0:0:1',NULL,NULL,155,'22',6,28,89,13,15940,16,12),
(8,NULL,NULL,NULL,NULL,42,'21',4,15,67,11,14390,1,1),
(9,'2023-05-16 13:45:41','172.26.12.139',NULL,NULL,25,'3',2,14,25,5,9059,12,8),
(10,NULL,NULL,NULL,NULL,26,'4',2,10,42,0,628,17,9),
(12,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(13,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(15,'2023-05-25 13:40:09','172.26.12.139',NULL,NULL,12,'2',4,8,8,0,283,2,9),
(16,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(27,'2023-05-19 08:39:48','172.26.12.139',NULL,NULL,0,'0',0,0,0,0,0,0,0),
(28,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(29,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(33,'2023-05-19 17:05:19','172.26.12.139',NULL,NULL,2,'0',1,2,0,0,530,0,2),
(34,'2023-05-19 16:56:20','172.26.12.139',NULL,NULL,0,'0',0,0,0,0,0,0,0),
(35,'2023-05-19 16:58:30','172.26.12.139',NULL,NULL,0,'0',1,0,0,0,321,0,0),
(36,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(37,'2023-05-23 12:25:43','172.26.12.139',1600,1700,51,'15',7,21,71,0,3399,5,75),
(38,'2023-05-19 19:34:24','172.26.12.139',403,403,0,'0',2,0,0,0,517,0,0),
(39,'2023-05-21 00:05:09','172.26.12.139',2453,2453,0,'0',1,0,0,0,2453,0,0),
(40,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(41,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(42,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(43,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(44,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(45,'2023-05-24 16:38:37','172.26.12.139',NULL,NULL,0,'0',0,0,0,0,0,0,0),
(46,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(49,NULL,NULL,NULL,NULL,0,'0',0,0,0,0,0,0,0),
(50,'2023-05-24 17:48:21','172.26.12.139',NULL,NULL,3,'1',1,0,0,0,0,0,0);
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
(27,0),
(28,0),
(29,0),
(33,0),
(34,0),
(35,0),
(36,0),
(37,0),
(38,0),
(39,0),
(40,0),
(41,0),
(42,0),
(43,0),
(44,0),
(45,0),
(46,0),
(49,0),
(50,0);
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

-- Dump completed on 2023-05-26  7:54:21
