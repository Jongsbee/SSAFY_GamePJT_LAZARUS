# Kubernetes 환경 구축

<details>
    <summary><b>CRI(컨테이너 런타임 인터페이스)란?</b></summary>

![image](https://user-images.githubusercontent.com/47595515/233936632-4f1f5a8f-04f5-4857-b604-74d719f727ad.png)

![image](https://user-images.githubusercontent.com/47595515/233936683-5b1b8020-57e6-45c6-90e4-a16188c2682f.png)

- 초기 쿠버네티스는 dockershim이라는 계층이 존재하여요구 사항에 맞게 Docker API를 호출하고, Docker에서 반환된 결과를 다시 쿠버네티스에 전달하는 방식으로 컨테이너 관리를 하였다.
- 하지만 Docker의 컨테이너 관리 기능만 필요했던 쿠버네티스에게 Docker Daemon은 너무 무거웠고 이에 쿠버네티스에서 새로운 인터페이스를 만들었는데 그것이 CRI이다.

  ![image](https://user-images.githubusercontent.com/47595515/233936708-cf2aae3b-20fc-4fc7-9065-44e60bc95f4b.png)

    - Dockershim은 쿠버네티스 버전 1.24부터 공식적으로 지원하지 않는다.
  
- CRI는 containerd(컨테이너디)와 CRI-O로 나뉜다.
    - containerd는 현재 Docker에서 사용하는 인터페이스이며 기능이 다양하고 Docker와 함께 사용할 수 있다는 장점이 있다.
    - CRI-O는 경량화된 인터페이스이며 쿠버네티스 전용으로, 필요한 기능들만 포함되어 있다. 부하가 적고 리소스 사용이 효율적이라는 장점이 있다. 이 글에서는 CRI-O를 이용하여 쿠버네티스 환경을 구성한다.
</details>