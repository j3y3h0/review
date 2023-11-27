# 자주쓰는 리눅스 명령어 모음

```bash
# centos 리눅스 버전 확인
# CentOS Linux release 7.9.2009 (Core)
cat /etc/centos-release

# sudo 권한부여
sudo visudo
username ALL=(ALL) NOPASSWD: ALL

# 전체 디스크 용량 확인
df -h

# 디렉토리 용량 확인
sudo du -sh /db-backup

# 프로세스 상태 확인
ps -ef

# telnet 설치 사용
sudo yum install telnet
telnet [IP_ADDESS] [PORT]

# 네트워크 설정 및 확인
sudo firewall-cmd --zone=public --add-port=[PORT]/tcp --permanent
sudo firewall-cmd --reload
sudo firewall-cmd --list-ports

# 포트상태 확인
sudo netstat -ntlp

# nginx 재시작
sudo systemctl restart nginx
sudo nginx -t
sudo nginx -s reload
sudo systemctl status nginx

# 전체 메모리 사용량 조회
free -m
# 시스템에서 가장 많은 자원을 사용하는 프로세스 조회
top

# 시스템의 전반적인 메모리 사용량, 프로세스, 페이징 등의 통계 조회
vmstat

# Docker 관련 명령어
docker stop CONTAINER_ID # 중단
docker start CONTAINER_ID # 실행
docker rm CONTAINER_ID # 컨테이너 삭제
docker rmi CONTAINER_ID # 이미지 삭제

# 컨테이너 로그 확인
docker logs CONTAINER_ID

# 해당 컨테이너 내부 환경으로 접속
docker exec -it CONTAINER_ID /bin/bash

# Dockerfile을 통해서 도커 이미지 빌드
docker build -t IMAGE_NAME .

# 빌드된 이미지로 컨테이너 실행
docker run -v /etc/localtime:/etc/localtime:ro -e TZ=Asia/Seoul -p 1234:5678 --name CONTAINER_NAME -d IMAGE_NAME:latest

# 현재 안쓰는 도커 이미지들 삭제
docker images | grep "<none>" | awk '{print $3}' | xargs docker rmi

```
