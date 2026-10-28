# 기본 씬 구성

## 씬과 모바일 화면

- 시작 씬: `Assets/Scenes/MainScene.unity`
- 대상: Android + iOS, 세로 방향 고정(Portrait).
- UI: Screen Space - Overlay Canvas, 기준 해상도 1080×1920, CanvasScaler의 Match Width Or Height 0.5.
- Canvas 바로 아래 `SafeArea`에 `SafeAreaPanel`을 연결했다. 런타임에 `Screen.safeArea`와 화면 크기 변경을 감지해 앵커를 갱신한다.
- 템플릿의 `Mobile_RPAsset`과 `Mobile` 품질 설정을 적용했다.
- 기존 SampleScene은 보존하고, 공통 빌드 씬 목록에는 MainScene을 등록했다.
- 빌드 프로필: `Assets/Settings/Build Profiles/SpoonBreak_Android.asset`, `SpoonBreak_iOS.asset`. 두 프로필은 공통 씬 목록(MainScene)을 사용한다.
- 기본 활성 빌드 타겟과 프로필은 Android / SpoonBreak_Android다. iOS로 빌드할 때는 SpoonBreak_iOS 프로필로 전환한다.
- Android Build Support와 SDK·NDK·OpenJDK, iOS Build Support를 6000.3.27f1에 설치했다. iOS 최종 빌드·서명에는 Mac과 Xcode가 필요하다.

## 하이어라키

```text
PrototypeWorld_TEMP
  PrisonCell_TEMP       감방 바닥·단면 벽·침대·카펫·철창
  DiggingFace_TEMP      굴착면·터널 입구·죄수·숟가락·돌
Directional Light
Main Camera            화면 배경과 AudioListener
CellCamera             감방 고정 시점 → CellView_TEMP RenderTexture
DigCamera              굴착면 고정 시점 → DigView_TEMP RenderTexture
GameCanvas
  Background
  SafeArea             SafeAreaPanel
    BannerSlot_TEMP
    GameTitle / PrisonName / TissueCount_TEMP / Income_TEMP
    CellView / CellTapArea / CellHint
    TunnelFold
    DigView / SpoonName_TEMP
    DepthTrack / DepthMarker_TEMP / DepthValue_TEMP / DepthGoal_TEMP
    DigToggle / SafetyHint_TEMP
    BottomBar / SpoonUpgradeTab / PrisonersTab / ShopTab / SettingsTab
EventSystem            InputSystemUIInputModule
```

UI의 감방·굴착면 RawImage가 각 카메라의 RenderTexture를 표시한다. 게임 화면에서 카메라를 조작하는 코드는 없다. 기본 씬 구성 단계이므로 게임 상태에 따른 카메라 이동·탈출 연출은 아직 연결하지 않았다.

## 임시값과 미구현 동작

- `_TEMP` 오브젝트·재질·텍스처는 화면 배치를 확인하기 위한 임시 리소스다.
- 표시된 휴지 0·초당 0·깊이 0m·숟가락 1단계·안전 안내는 정적인 초기 화면 표시다. 경제·강화·간수·파기 로직과 연결하지 않았다.
- 탈출 깊이는 아직 정하지 않아 `준비 중`으로 표시한다. 깊이 마커 위치도 임시다.
- 배너는 레이아웃 확인용 자리이며 광고 SDK가 연결되지 않았다.
- 감방 탭·하단 메뉴는 Button 컴포넌트이며 게임 동작은 미연결이다. 파기 Toggle의 시각적 체크 상태도 실제 파기·카펫·안전 상태에 연결되지 않았다.
- 두 RenderTexture는 각각 720×740, 720×560, MSAA 1이다. 목표 기기 성능과 최종 화면 비율에 맞춰 추후 조정한다.

## 글꼴

- 한글 UI는 Noto Sans CJK KR Regular를 사용한다.
- 원본: https://github.com/notofonts/noto-cjk/tree/main/Sans/OTF/Korean
- 라이선스: `Assets/Fonts/NotoSansKR/LICENSE.txt`.
- 현재 씬 문자열과 숫자·영문을 포함한 정적 TMP 아틀라스를 사용한다. 이후 문구를 추가할 때 글리프를 추가하거나 필요한 fallback을 구성한다.

## 검증 범위

- 에디터 Play 모드의 프레임 진행을 확인했다.
- 540×960(9:16)과 540×1200(9:20) 화면에서 한글과 기본 UI 배치를 확인했다.
- 실제 기기·노치가 있는 기기에서의 안전 영역 및 터치 검증, Android 앱 빌드와 iOS Xcode 빌드·서명은 아직 수행하지 않았다.
- Pipeline `capture screenshot`은 카메라만 렌더링하므로 Overlay UI가 누락된다. 전체 UI 확인은 Play 모드에서 `ScreenCapture.CaptureScreenshot`을 사용했다.
