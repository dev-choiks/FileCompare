# (C# 코딩) 파일 비교 툴 (File Compare)



## 개요
- C# 프로그래밍 학습
- 1줄 소개: 두 개의 폴더를 선택하여 내부의 파일들을 비교하고 상호 복사 및 관리하는 파일 비교 프로그램
- 사용한 플랫폼:
  - C#, .NET Windows Forms, Visual Studio, GitHub
- 사용한 컨트롤:
  - SplitContainer, Panel, ListView, TextBox, Button
- 사용한 기술과 구현한 기능:
  - SplitContainer와 Panel, Dock/Anchor 속성을 활용한 반응형 분할 화면 UI 디자인
  - FolderBrowserDialog를 활용한 시스템 폴더 경로 탐색 및 선택 기능
  - Directory.EnumerateFiles 및 FileInfo 클래스를 이용한 폴더 내 파일 목록 및 상세 정보 추출
  - ListView 컨트롤을 활용한 파일 정보(이름, 크기, 수정일)의 표 형태(Details) 출력



## 실행 화면 (과제1)
- 과제1 코드의 실행 스크린샷




![과제1 실행화면1](img/1.png)




![과제1 실행화면2](img/2.png)




- 과제 내용
  - 7주차 파일 비교 툴(File Compare)의 뼈대가 되는 기본 UI 레이아웃을 배치합니다.
  - 창 크기를 조절해도 컨트롤들이 깨지지 않도록 Dock과 Anchor 속성을 적절히 설정합니다.
  - 파일 목록을 표시할 양쪽 ListView 컨트롤을 표(Details) 형태로 초기화하고 열(Column)을 추가합니다.

- 구현 내용과 기능 설명
  - `SplitContainer`(`spcMain`)를 폼 전체에 `Dock = Fill`로 배치하여 화면을 좌우 대칭 구역으로 분할했습니다.
  - 좌우 구역 내부에 `Panel`을 활용하여 구조화했습니다. 상단 메뉴 패널(`pnlLeftMenu`, `pnlRightMenu`)은 `Dock = Top`으로 고정하고, 하단 리스트 패널(`pnlLeftList`, `pnlRightList`)은 `Dock = Fill`로 채워 화면 공간을 효율적으로 구성했습니다.
  - 경로를 표시하는 `TextBox`는 `Anchor = Top, Left, Right`로, 폴더 선택 `Button`은 `Anchor = Top, Right`로 설정하여 사용자가 윈도우 창 크기를 가로로 늘리거나 줄일 때 컨트롤들이 자연스럽게 반응하며 함께 크기가 조절되도록 UX를 개선했습니다.
  - `Form1` 생성자에서 양쪽 `ListView`(`lvwLeftDir`, `lvwRightDir`)의 `View` 속성을 `Details`로 코딩하여 표 형태로 설정하고, `FullRowSelect`와 `GridLines`를 활성화한 뒤 '이름', '크기', '수정일' 열(Column)을 추가하는 초기화 로직을 구현했습니다.