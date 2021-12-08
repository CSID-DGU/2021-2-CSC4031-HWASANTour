FROM openjdk:11
# 환경 변수 선언 - JAR File 생성 위치를 변수로 선언
ARG JAR_FILE=build/libs/*.jar
# JAR 파일을 app.jar 이름으로 복사, 실제 실행 대상 JAR 파일명을 통일하기 위함
# Container화 할 때 JAR 파일명이 매번 달라지는 경우 문제 해결
COPY ${JAR_FILE} app-metaverse-api.jar
ENTRYPOINT ["java","-jar","/app-metaverse-api.jar"]