## SET ANSI_NULLS ON/OFF 

[SET](https://blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=kimaudgml&logNo=90118652591)   

NULL 값에 대한 비교 처리를 표준에 따를 것인지 구분   
  - 컬럼 = NULL 은 비표준임(OFF 시 사용 가능한 표현, ON 에서 사용시 오동작)
  - 컬럼 IS NULL 은 표준임(권장)

## SET QUOTED_IDENTIFIER ON/OFF

   따옴표 처리를 표준에 따를 것인지 여부   
  - SELECT "1" 은 비표준임(OFF 시 사용 가능한 표현, ON 에서 오류)
  - SELECT '1' 은 표준임(권장)

   
   
## 트랜잭션 격리 수준   

[격리수준](https://kuaaan.tistory.com/98)   

- READ COMMITTED : 커밋된 데이터만 읽을 수 있는 격리수준

- READ UNCOMMITED : 커밋되지 않은 데이터도 읽을 수 있는 격리수준

- REPEATABLE READ : SELECT했을 때 해당 ROW에 걸리는 S-LOCK이 즉시 해제되지 않고 트랜잭션 종료시까지 유지되는 것이다.

## SET NOCOUNT {ON/OFF}

- "N개의 행이 영향을 받음" 메시지 안뜨게 함