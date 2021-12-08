-- 'metaverse' 데이터베이스 생성
CREATE DATABASE IF NOT EXISTS metaverse;

-- 'docker' 유저 생성 & 컨테이너 내 데이터베이스 접근 가능하도록 권한 설정
USE mysql;

REVOKE ALL PRIVILEGES, GRANT OPTION FROM 'docker'@'%';

DELETE FROM mysql.user WHERE user='docker';

FLUSH PRIVILEGES;


CREATE USER 'docker'@'%' IDENTIFIED BY 'docker';

GRANT ALL ON metaverse.* TO 'docker'@'%';

FLUSH PRIVILEGES;