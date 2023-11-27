# Git 명령어 모음

- git --version : 설치 확인

- git init

- git remote add origin 원격저장소주소 : 원격 저장소에 연결

- git remote -v : 원격 저장소에 잘 연결되었는지 확인

- git remote show [remote-name] : 레포지토리 정보 조회

- git clone URL

- git status : 깃 상태 확인

- git config --global user.name "유저 이름" : 깃 사용자 이름 설정

- git config --global user.email "이메일 주소" : 깃 사용자 이메일 설정

- git config --global core.editor "vim" : 커밋 편집에디터를 vim으로 변경하기

- git add . : 현재 폴더 내의 모든 변경사항을 stage에 추가

- git commit -m "메세지 내용" : 메세지와 함께 커밋하기

- git commit -am "메세지 내용" : 스테이징과 커밋을 메세지와 함께 올리기

- git push -u origin master : 지역 저장소의 브랜치를 원격 저장소의 마스터 브랜치와 연결 (한번만 하면됨)

- git push : 원격 저장소에 올리기

- git branch : 브랜치 확인

- git branch 브랜치이름 : '브랜치이름'으로 브랜치 만들기

- git branch -d 삭제할브랜치이름 : 브랜치 삭제(마스터 브랜치에서 해야한다.)

- git checkout 브랜치이름 : '브랜치이름'으로 브랜치 이동

- git log 브랜치1 ..브랜치2 : 브랜치1과 브랜치2사이의 차이점 보기

- git merge 병합할브랜치이름 : 브랜치 병합

- git log : 커밋 기록 보기

- git log --stat : 커밋 기록을 커밋에 관련괸 파일과 함께 보기

- git clone 원격저장소주소 지역저장소디렉토리 : 원격저장소 가져오기

- git pull origin master : 원격 저장소의 내용을 지역 저장소의 마스터브랜치로 가져오기
- git fetch origin : 원격 저장소의 브랜치 변화 정보만 가져오기

- git reset --hard HEAD : 작업 디렉토리에 모든 변경 버리기

- git revert <commit> : 커밋 되돌아가기

  ![Git Flow](./git-flow.png)
