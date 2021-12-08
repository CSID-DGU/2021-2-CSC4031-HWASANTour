-- MySQL dump 10.13  Distrib 8.0.26, for macos11.3 (x86_64)
--
-- Host: localhost    Database: metaverse
-- ------------------------------------------------------
-- Server version	8.0.26

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `notice`
--

DROP TABLE IF EXISTS `notice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `notice` (
  `id` bigint NOT NULL AUTO_INCREMENT COMMENT '학사정보 조회 ID',
  `title` varchar(100) NOT NULL COMMENT '학사정보 게시글 타이틀',
  `count_view` int unsigned NOT NULL DEFAULT '0' COMMENT '학사정보 게시글 조회수',
  `create_date` date NOT NULL COMMENT '학사정보 게시글 작성일',
  `fetch_date` date NOT NULL COMMENT '학사정보 데이터 조회 일시',
  `notice_url` varchar(500) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=281 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notice`
--

LOCK TABLES `notice` WRITE;
/*!40000 ALTER TABLE `notice` DISABLE KEYS */;
INSERT INTO `notice` VALUES (271,'[기타]11월 26일 모의토익 고사장 및 성적 확인 방...',49,'2021-11-25','2021-11-25','https://www.dongguk.edu/mbs/kr/jsp/board/view.jsp?spage=1&boardId=3638&boardSeq=26741023&id=kr_010801000000&column=&search=&categoryDepth=&mcategoryId=0'),(272,'[계절학기] 2021 겨울계절학기 1차 폐강강좌 확정 및 ...',661,'2021-11-24','2021-11-25','https://www.dongguk.edu/mbs/kr/jsp/board/view.jsp?spage=1&boardId=3638&boardSeq=26740987&id=kr_010801000000&column=&search=&categoryDepth=&mcategoryId=0'),(273,'[학적][2022-1학기] 카운슬링센터 재입학 상담 안...',190,'2021-11-24','2021-11-25','https://www.dongguk.edu/mbs/kr/jsp/board/view.jsp?spage=1&boardId=3638&boardSeq=26740983&id=kr_010801000000&column=&search=&categoryDepth=&mcategoryId=0'),(274,'[프로그램및특강]2021 Global Capstone Desig...',487,'2021-11-23','2021-11-25','https://www.dongguk.edu/mbs/kr/jsp/board/view.jsp?spage=1&boardId=3638&boardSeq=26740922&id=kr_010801000000&column=&search=&categoryDepth=&mcategoryId=0'),(275,'[학적]2022학년도 1학기 전과(전공변경)제도 시행 ...',783,'2021-11-22','2021-11-25','https://www.dongguk.edu/mbs/kr/jsp/board/view.jsp?spage=1&boardId=3638&boardSeq=26740900&id=kr_010801000000&column=&search=&categoryDepth=&mcategoryId=0'),(276,'[학적]2022학년도 봄 졸업대상자 졸업연기 신청 안내...',513,'2021-11-22','2021-11-25','https://www.dongguk.edu/mbs/kr/jsp/board/view.jsp?spage=1&boardId=3638&boardSeq=26740899&id=kr_010801000000&column=&search=&categoryDepth=&mcategoryId=0'),(277,'[계절학기]2021학년도 겨울계절학기 시행 안내',10487,'2021-11-09','2021-11-25','https://www.dongguk.edu/mbs/kr/jsp/board/view.jsp?spage=1&boardId=3638&boardSeq=26740557&id=kr_010801000000&column=&search=&categoryDepth=&mcategoryId=0'),(278,'[수업,성적]2021-겨울학기 사회봉사교과목 수강신청 안내',1380,'2021-11-09','2021-11-25','https://www.dongguk.edu/mbs/kr/jsp/board/view.jsp?spage=1&boardId=3638&boardSeq=26740532&id=kr_010801000000&column=&search=&categoryDepth=&mcategoryId=0'),(279,'[학적]2022학년도 1학기 재입학 신청 안내',1809,'2021-11-04','2021-11-25','https://www.dongguk.edu/mbs/kr/jsp/board/view.jsp?spage=1&boardId=3638&boardSeq=26740461&id=kr_010801000000&column=&search=&categoryDepth=&mcategoryId=0'),(280,'[수업,성적]2021-겨울학기 현장실습 참가학생 모집',2940,'2021-11-04','2021-11-25','https://www.dongguk.edu/mbs/kr/jsp/board/view.jsp?spage=1&boardId=3638&boardSeq=26740453&id=kr_010801000000&column=&search=&categoryDepth=&mcategoryId=0');
/*!40000 ALTER TABLE `notice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `board`
--

DROP TABLE IF EXISTS `board`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `board` (
  `id` bigint NOT NULL AUTO_INCREMENT COMMENT '코끼리스타그램 게시글 ID',
  `member_id` varchar(45) NOT NULL COMMENT '코끼리스타그램 업로드 유저',
  `img_dir` varchar(100) NOT NULL COMMENT '코끼리스타그램 이미지 저장 위치 on AWS S3',
  `create_date` datetime NOT NULL COMMENT '코끼리스타그램 업로드 일시',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `board`
--

LOCK TABLES `board` WRITE;
/*!40000 ALTER TABLE `board` DISABLE KEYS */;
INSERT INTO `board` VALUES (1,'username1','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/14.png','2021-10-22 20:15:37'),(2,'username2','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/2.png','2021-10-22 20:16:03'),(3,'username1','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/3.png','2021-10-22 22:54:08'),(4,'username1','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/4.png','2021-10-22 22:54:11'),(5,'username1','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/5.png','2021-10-22 22:54:15'),(6,'username1','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/6.jpeg','2021-10-22 22:54:13'),(7,'username1','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/7.jpeg','2021-10-22 22:54:16'),(8,'username1','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/8.jpeg','2021-10-22 22:54:19'),(9,'username1','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/9.jpeg','2021-10-22 22:54:18'),(10,'username1','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/10.jpeg','2021-10-22 22:54:21'),(11,'username1','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/11.jpeg','2021-10-22 22:54:22'),(12,'username1','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/12.jpeg','2021-10-22 22:54:23'),(13,'username1','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/13.jpeg','2021-10-22 22:54:24'),(14,'username1','https://ai-photo-zone.s3.ap-northeast-2.amazonaws.com/static/1.jpeg','2021-11-25 23:37:15');
/*!40000 ALTER TABLE `board` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-11-27 15:05:07
