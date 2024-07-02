# 유니티 InGameDebugger

[GitHub repository](https://github.com/SexyDu/InGameDebugger)

## 개요
* 인게임콘솔 및 CLI 기능으로 디버깅에 도움을 주기 위한 목적으로 만들어진 도구
* 2가지 형식의 콘솔 인터페이스 제공 (아래 이미지 참고)
![Unity InGameDebugger](https://github.com/SexyDu/InGameDebugger/assets/128912129/38e942da-a66c-4513-afbd-42427d1df3bd)

## 기능
![InGameDebugger Feature](https://github.com/SexyDu/InGameDebugger/assets/128912129/7cc25879-56e6-4753-9d50-7a9f9173f773)
1. 로그 수집 실행/정지
2. 콘솔 클리어
3. 콘솔 필터
![Feature Filter](https://github.com/SexyDu/InGameDebugger/assets/128912129/c734dccd-8fa9-4c83-8069-cb1687c0b41a)
4. 터미널(CLI)
![Feature CLI](https://github.com/SexyDu/InGameDebugger/assets/128912129/6ab27ca8-5511-403b-b3d9-2651d6a8ad21)
5. 앵커 변경
![Feature Anchor](https://github.com/SexyDu/InGameDebugger/assets/128912129/6bf200ad-11aa-436c-a517-0cfc3e7c963e)
6. 콘솔창 최소화 종료
7. (아이템 형식 콘솔) 로그 상세
<br>7-1. 로그 상세 문자열 Copy
8. (텍스트 형식 콘솔) StackTrace 정보 활성화/비활성화

## 콘솔 활성화
* 지정된 터치 수(FingerCount)를 지정된 입력시간(PressureTime)동안 유지 수 활성화 (InGameDebuggerSettings asset의 ActivationInfo 설정 내용 기반)
  - <b>(기본) 손가락 3개로 화면 터치 후 0.5초 유지 시 콘솔 활성화</b>
* [에디터 전용] 지정된 키(KeyCode)를 지정된 입력시간(PressureTime)동안 유지 수 활성화 (InGameDebuggerSettings asset의 ActivationInfo 설정 내용 기반)
  - <b>(기본) BackQuote(`) 키를 0.5초간 누르고 있으면 콘솔 활성화</b>

## 설정
InGameDebuggerSettings asset을 통한 설정
* InGameDebuggerSettings.cs 내용 참고

![Settomgs](https://github.com/SexyDu/InGameDebugger/assets/128912129/57077fd6-187c-463b-b7c9-4116dea418fb)
