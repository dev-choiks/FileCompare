# (C# 코딩) 파일 비교 툴 (File Compare)

## 개요
- C# 프로그래밍 학습
- 1줄 소개: 두 개의 폴더를 선택하여 내부의 파일들을 비교하고 수정 날짜를 기준으로 상태를 색상으로 구분해 주는 반응형 파일 비교 프로그램
- 사용한 플랫폼:
  - C#, .NET Windows Forms, Visual Studio, GitHub
- 사용한 컨트롤:
  - SplitContainer, Panel, ListView, TextBox, Button
- 사용한 기술과 구현한 기능:
  - SplitContainer와 Panel, Dock/Anchor 속성을 활용한 반응형 분할 화면 UI 디자인
  - FolderBrowserDialog를 활용한 시스템 폴더 경로 탐색 및 선택 기능
  - 메모리 효율을 높인 Directory.EnumerateFiles 및 FileInfo 클래스를 이용한 폴더 내 하위 파일 목록 추출
  - try-catch 구문을 활용한 예외 처리(DirectoryNotFoundException, IOException) 안전망 구축
  - 두 파일의 LastWriteTime(수정 날짜) 속성을 비교하여 결과(최신, 과거, 동일, 단독)에 따라 폰트 색상을 동적으로 변경(ForeColor 제어)

## 실행 화면 (과제1)
- 과제1 코드의 실행 스크린샷

![과제1 실행화면1](img/1.png)

![과제1 실행화면2](img/2.png)

- 과제 내용
  - 7주차 파일 비교 툴(File Compare)의 뼈대가 되는 기본 UI 레이아웃을 배치합니다.
  - 창 크기를 조절해도 컨트롤들이 깨지지 않고 자연스럽게 반응하도록 Dock과 Anchor 속성을 적절히 설정합니다.
  - 파일 목록을 표시할 양쪽 ListView 컨트롤을 표(Details) 형태로 초기화하고 열(Column)을 추가합니다.

- 구현 내용과 기능 설명
  - `SplitContainer`(`spcMain`)를 폼 전체에 `Dock = Fill`로 배치하여 화면을 좌우 대칭 구역으로 분할했습니다.
  - 좌우 구역 내부에 `Panel`을 활용하여 구조화했습니다. 상단 메뉴 패널(`pnlLeftMenu`, `pnlRightMenu`)은 `Dock = Top`으로 고정하고, 하단 리스트 패널(`pnlLeftList`, `pnlRightList`)은 `Dock = Fill`로 채워 공간을 효율적으로 구성했습니다.
  - 경로 표시용 `TextBox`는 `Anchor = Top, Left, Right`로, 폴더 선택 `Button`은 `Anchor = Top, Right`로 설정하여 사용자가 윈도우 창 크기를 가로로 늘리거나 줄일 때 컨트롤이 함께 늘어나고 이동하도록 UX를 개선했습니다.
  - `Form1` 생성자에서 양쪽 `ListView`(`lvwLeftDir`, `lvwRightDir`)의 `View` 속성을 `Details`로 코딩하여 표 형태로 설정하고, `FullRowSelect`와 `GridLines`를 활성화한 뒤 '이름', '크기', '수정일' 열(Column)을 추가하는 초기화 로직을 구현했습니다.

## 실행 화면 (과제2)
- 과제2 코드의 실행 스크린샷

![과제2 실행화면1](img/3.png)

![과제2 실행화면2](img/4.png)

![과제2 실행화면3](img/5.png)

- 과제 내용
  - 폴더 선택 버튼을 누르면 `FolderBrowserDialog`를 띄워 사용자가 디렉터리 경로를 지정할 수 있게 만듭니다.
  - 선택한 경로 안의 파일 목록을 읽어와 `ListView`에 파일명, 크기, 수정일로 상세히 나열합니다.
  - 양쪽 폴더에 파일이 모두 로드되면 동일한 이름의 파일을 찾아 수정 시간(날짜)에 따라 최신, 과거, 단독 파일 여부를 각각 다른 색상으로 구분하여 표시합니다.

- 구현 내용과 기능 설명
  - 대량의 파일을 처리할 때 성능과 메모리 효율을 높이기 위해 `GetFiles` 대신 데이터 스트리밍 방식인 `Directory.EnumerateFiles` 메서드를 활용하여 리스트뷰에 정보를 출력했습니다.
  - 디렉터리 접근 권한 오류나 경로를 찾을 수 없는 문제로 프로그램이 강제 종료되는 것을 방지하기 위해 `try-catch-finally` 구문을 적용했습니다.
  - 양쪽 리스트뷰에 아이템이 추가될 때마다 `CompareFiles()` 사용자 정의 메서드를 호출하여 파일들의 `LastWriteTime`을 상호 비교했습니다. 비교 결과에 따라 `ForeColor` 속성을 변경하여, 내용과 날짜가 완전히 동일한 파일은 검은색, 최신 파일은 빨간색, 구버전 파일은 회색, 한쪽에만 존재하는 단독 파일은 보라색(Purple)으로 폰트 색상이 즉각적으로 적용되도록 시각적 비교 기능을 완성했습니다.