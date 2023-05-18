# LAZARUS

![image](https://user-images.githubusercontent.com/104764340/233580330-47909bab-0033-4f1a-88ed-ac8483d88130.png)


<br/>

### 🎞 프로젝트 개요

- 설명  : 3D 메타버스 세상 속 서바이벌 게임 
- 특징  : 높은 실사도와 다양한 기능을 통해 유저의 게임 몰입도를 높임

<br/>

### 💡 프로젝트 특징

- 높은 퀄리티의 배경과 애니메이션으로 몰입도 증진
- 리얼리티 + 카툰풍 과 긴장감있는 게임 플레이의 조화
- 게임플레이 로그를 기록하고 통계를 내어 유저에게 제공

<br/>

### ✔ 주요 기능



<br/>

### 📅 프로젝트 진행 기간

2023.04.10일(월) ~ 2023.05.19(금)

<br/>

## 💛 팀 소개

- **김종섭**: Unity(클라이언트), PM, GM, Background 및 UI 디자인, API 설계 및 관리, 코드품질 관리 및 리팩토링, 렌더링 파이프라인
- **이정규**: Unity(클라이언트) 리더, 문서정리, 게임기획, 동물 및 몬스터 AI, SFX, 프로젝트 구조 설계
- **정재영**: Unity(클라이언트), Player 전체(공격, 이동, 사망, 애니메이션, 커스터마이징), 게임플레이 및 디버깅
- **정해석**: Unity(클라이언트), UI 연동, 이벤트 컨트롤, 스테이터스, 아이템 + 조합
- **박윤환**: Backend 리더, DevOps 담당, 아키텍처 설계, REST API 개발
- **이승헌**: Backend, Frontend(Vue.js), ERD 및 API 설계, REST API 개발

<br/>

## ⚙ 개발 환경

🔧 **Backend**

- IntelliJ : 2022.3.1 (Ultimate Edition)
- Open JDK 11
- Spring Boot 2.7.11
- Spring Data JPA

🔧 **Frontend**
- Unity 2021.3.22
- Vue.js 2.7.14

🔧 **CI/CD**

- AWS EC2 Ubuntu 20.04 LTS
- Kubernetes 1.26.2
- CRI-O 1.26.1
- Nginx Ingress Controller 1.6.4
- Jenkins : 2.404
- ArgoCD: 2.7.1

🔧 **DB**
- MariaDB 10.11.2
- Redis 7.0.11
- MongoDB 6.0.5


<br/>

## 🗂 프로젝트 폴더 구조

- Frontend
```text
admin-front
├── public
└── src
    ├── assets
    │   └── css
    ├── components
    │   └── common
    ├── router
    └── views
```

- Backend - MainServer
```text
mainserver
├── api
│   ├── controller
│   └── service
├── common
│   ├── exception
│   └── logger
├── config
├── db
│   ├── entity
│   └── repository
├── dto
│   ├── enums
│   ├── request
│   └── response
└── util
```

- Backend - LogServer
```text
logserver
├── api
│   ├── controller
│   └── service
├── common
│   ├── exception
│   └── logger
├── config
├── db
│   ├── entity
│   └── repository
└── dto
    ├── enums
    ├── request
    └── response
```

- Backend - SchedulerServer
```text
schedulerserver
├── db
│   ├── entity
│   └── repository
├── dto
│   └── enums
└── scheduler
```

<br/>

## 🗺 서비스 아키텍처

<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/47595515/239277788-206fd9c8-bdf0-421e-945e-c997c47da3e9.png" width="600"/>

<br/>

## 📜 기능 명세서

<details>
    <summary>Swagger-ui</summary>
    <img src="https://github-production-user-asset-6210df.s3.amazonaws.com/47595515/239291358-412b440a-3e58-45dd-be96-371cbca71cb1.jpeg" width="600"/>

</details>


<br/>

## 📊 ERD

<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/47595515/239280235-c7941ac6-e85d-4a9c-8bbf-cce997d09180.png" width="800"/>

<br/>

### 🤝 컨벤션

<details>
    <summary><b>git 컨벤션</b></summary>

```text
### 제목
# :gitmoji: [FE/BE/공통] 작업내용 (제목과 본문은 한 줄 띄워주세요)


### 본문 - 한 줄에 최대 72 글자까지만 입력하기
# 무엇을, 왜, 어떻게 했는지


# 꼬리말
# (선택) 이슈번호 작성

#   [커밋 타입]  리스트
#   :sparkles:          : 기능 (새로운 기능)
#   :bug:               : 버그 (버그 수정)
#   :lipstick:          : CSS 등 사용자 UI 디자인 변경
#   :recycle:           : 리팩토링
#   :art:               : 스타일 (코드 형식, 세미콜론 추가: 비즈니스 로직에 변경 없음)
#   :memo:              : 문서 (문서 추가, 수정, 삭제)
#   :white_check_mark:  : 테스트 (테스트 코드 추가, 수정, 삭제: 비즈니스 로직에 변경 없음)
#   :hammer:            : 기타 변경사항 (빌드 스크립트 수정 등)
#   :truck:             : 파일 혹은 폴더명을 수정하거나 옮기는 작업만 하는 경우
#   :fire:              : 코드, 파일을 삭제하는 작업만 수행한 경우
#   :twisted_rightwards_arrows:    : 브랜치 합병
#   :rocket:            : 배포 관련
# ------------------
#   [체크리스트]
#     제목 첫 글자는 대문자로 작성했나요?
#     제목은 명령문으로 작성했나요?
#     제목 끝에 마침표(.) 금지
#     제목과 본문을 한 줄 띄워 분리하기
#     본문에 여러줄의 메시지를 작성할 땐 "-"로 구분했나요?
# ------------------
```
  
</details>

<details>
    <summary><b>브랜치 전략</b></summary>

```
⭐main
  - dev
    - dev-front
      - feature-front/기능명
    - dev-back
      - api-server/기능명
      - scheduler-server/기능명
    - fix : 문제가 생긴 브랜치에서 분기
      - fix-front/기능명
      - fix-back/기능명
  - docs/문서타입[ex) README, exec]
```

</details>

<details>
    <summary><b>Backend 코드 컨벤션</b></summary>

  - 명명법
     - 변수명, 메서드명
         - `camelCase`
     - 의미없는 변수명 사용 지양 → 유지보수의 어려움
     - 메서드 이름은 소문자로 시작, 동사 → ex) getName()
     - 클래스 이름은 대문자로 시작
  - 코딩 스타일 자동적용 설정
    - IntelliJ에 NAVER [캠퍼스 핵데이 Java 코딩 컨벤션](https://naver.github.io/hackday-conventions-java/) 적용
    - 저장시 액션 설정
      <img src="https://user-images.githubusercontent.com/47595515/219520513-12db4f55-b814-4395-9c01-6207abab589f.png" width="600px">
  - 줄바꿈을 CRLF 대신 LF로 변경 (윈도우 한정)
    - 새로 만들어진 파일을 LF로 적용
        <img src="https://user-images.githubusercontent.com/96561194/230012393-1e34f2a1-6ac5-44dd-86c9-344acf268e45.png" width="600"/>
    - 기존 파일을 LF로 변경 방법
      - 현재 파일
          <img src="https://user-images.githubusercontent.com/96561194/230013239-ae5e8162-4275-4ea7-83d2-82850d25239a.png" width="600"/>
      - 디렉토리
        - 바꿀 디렉토리 선택
            <img src="https://user-images.githubusercontent.com/96561194/230014121-1bdc79b6-3679-4718-aa8a-7603fdf262ed.png" width="400"/>
        - 파일 - 파일 프로퍼티 - 줄 구분 기호 - LF 선택
            <img src="https://user-images.githubusercontent.com/96561194/230014384-e73de92f-ed4f-4a53-ad06-f8c9130d88d4.png" width="600"/>

</details>

<br/>

## 🎨 기능 상세 설명


## 📚 문서
- [📢 Notion](https://past-ring-03e.notion.site/Team-781bd1c81d78443fae3349826c4c3ce2)
