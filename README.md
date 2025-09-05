# 3D_Squad_Swarm  
  
## 🎮 게임소개
작은 맵에서 플레이어 vs NPC vs 좀비가 빠르게 충돌하는 라운드형 3D 액션입니다.  
모든 캐릭터는 상태머신(FSM) 으로 움직이며, 스테이지 설정(SO) 과 스폰 규칙으로 라운드 난이도를 제어합니다.  
UI는 인트로 → 스테이지선택 → 게임 → 일시정지/클리어/게임오버/타임업으로 전환됩니다.  

제한시간(Timer)과 감염도 게이지가 핵심 규칙  
 
적 처치 시 감염도 상승 → GameClear  
  
시간 초과 → TimeUP  

플레이어 사망 → GameOver  


## ▶️ 플레이영상
[시연 영상 보기](https://your.video.link)  

## 🕹️ 플레이방법  
게임 실행 → Intro 진입    
StageSelect에서 스테이지 선택  
Game 씬에서 제한시간 내 목표 달성  
상황에 따라 Pause / GameOver / GameClear / TimeUP으로 전환  
  
### 입력 (Unity Input System)  
액션 맵: PlayerInput.PlayerMovement  
이동: Movement 액션 (키보드/패드 지원)  
일시정지: Pause 액션 (예: Esc)    
근접공격: 적 접촉 시 자동 트리거 + 상태에 따라 Attack 전환  
  
## ✨ Features   
### 상태머신 기반 캐릭터 로직  
공통 FSM: StateMachine.cs  
Player FSM: PlayerStateMachine + PlayerIdle/Walk/Attack/DeathState   
NPC FSM: NPCBaseState, NPCAttackState (그 외 Idle/Chase/Flee/Death 등 폴더 포함)   
Zombie(Follower/Charging) FSM: ZombieStateMachine + ZombieIdle/Follow/Chasing/Charge/Attack/Death/Rise  
  
### Stage 시스템    
StageConfig(ScriptableObject)로 씬/스폰룰/네비 설정   
StageManager가 시작 이벤트 발행 → StageSpawner가 규칙대로 스폰 
NavMesh 전용 스폰: NavMeshData + NavMesh.SamplePosition 기반 안전 위치 선정    
   
### UI 파이프라인   
UIManager가 상태 전환(인트로/스테선/게임/일시정지/게임오버/클리어/타임업)  
GameUI, GameOverUI, GameClearUI, TimeUPUI, StageSelectUI, IntroUI  
타이머 중복 방지: 코루틴 핸들(_timerRoutine) 기반 시작/정지   
감염도/킬 카운트: StageManager.OnEnemyKilled → UIManager.getInfection  
  
### 전투/인터랙션  
IDamageable 인터페이스, Bullet, GunAimer(라인 에임), ForceReceiver   
AnimationEventForwarder / NPCAnimationEventForwarder로 애니메이션 이벤트 전달   
  
### 카메라/오디오   
CameraManager(Cinemachine), CameraDeathEffect, CameraOcclusion  
AudioManager + BGMVolumeController + SoundEffect (씬별 BGM 전환)  
  
### 스폰/유틸  
PlayerSpawner, StageSpawner  
Billboard, HoverEffect, HandleAnimationController  
  
## 🧩 설계 & 디자인패턴  
  
### State Pattern (FSM)  
각 도메인(Player/NPC/Zombie)에 독립 FSM을 두고, Enter/Exit/Update/HandleInput/PhysicsUpdate 수명주기로 관리.  
  
### Singleton  
Singleton<T>로 GameManager / StageManager / UIManager / AudioManager 등 전역 매니저 관리.  
   
### ScriptableObject 구성/데이터 주입  
StageConfig, ScriptableStats로 씬/스폰/스탯을 에셋화 → StageManager / StatHandler가 적용.  
  
### Event-Driven  
StageManager.OnStageStarted 이벤트로 스테이지 전환/스폰 동기화.  
   
### Animation Event Forwarding  
애니메이션 타이밍(사망 완료, 부활 등)을 스크립트로 안전하게 전달.  
  
### NavMesh 중심 설계   
스폰/이동/추적·도주를 네비 데이터로 일관되게 처리.  
  
  
## 🛠️ 사용기술 & 시스템  
   
Unity (LTS 권장), C#  
Unity NavMesh: NavMeshAgent, NavMeshObstacle(Carving), NavMeshData 스폰   
Unity Input System: PlayerInput 액션 맵(PlayerMovement)  
Cinemachine: 가상 카메라 전환/우선순위   
UI (UGUI): Image, Canvas 기반 화면 전환/게이지   
Audio: AudioSource, 씬별 BGM 스위칭  
ScriptableObject: 스테이지/스탯 파라미터 관리  
Coroutines: 타이머/페이드/스폰 루프   
기타 유틸: CharacterController, LineRenderer(에임), PostProcessing(옵션)  
  
  
## 🧱 실제 프로젝트 구조 (Assets/02. Scripts)   

```  
02. Scripts
├─ AnimationForwarder
│  ├─ AnimationEventForwarder.cs
│  └─ NPCAnimationEventForwarder.cs
│
├─ Base
│  ├─ StateMachine
│  │  └─ StateMachine.cs
│  ├─ Ui
│  │  └─ BaseUI.cs
│  └─ Zombie
│     └─ BaseZombie.cs
│
├─ Camera
│  ├─ CameraDeathEffect.cs
│  └─ CameraOcclusion.cs
│
├─ ForceReciever
│  └─ ForceReciever.cs
│
├─ Interface
│  └─ Attack
│     └─ IDamageable.cs
│
├─ Manager
│  ├─ Player
│  │  └─ PlayerManager.cs
│  ├─ Stage
│  │  └─ StageManager.cs
│  ├─ UI
│  │  └─ UIManager.cs
│  ├─ Util
│  │  ├─ AudioManager.cs
│  │  └─ CameraManager.cs
│  └─ Zombie
│     └─ ZombieManager.cs
│
├─ NPC
│  ├─ Gun
│  │  ├─ Bullet.cs
│  │  └─ GunAimer.cs
│  └─ StateMachin
│     ├─ NPCAttackState.cs
│     ├─ NPCBaseState.cs
│     ├─ NPCChaseState.cs
│     ├─ NPCDeathState.cs
│     ├─ NPCFleeState.cs
│     ├─ NPCGroundState.cs
│     ├─ NPCIdleState.cs
│     ├─ NPCStateMachine.cs
│     └─ NPCTest.cs
│
├─ Player
│  ├─ Anim
│  │  └─ PlayerAnimationData.cs
│  ├─ StateMachine
│  │  ├─ PlayerAttackState.cs
│  │  ├─ PlayerBaseState.cs
│  │  ├─ PlayerDeathState.cs
│  │  ├─ PlayerGroundState.cs
│  │  ├─ PlayerIdleState.cs
│  │  ├─ PlayerStateMachine.cs
│  │  └─ PlayerWalkState.cs
│  ├─ EnemyPointer.cs
│  ├─ Player.cs
│  └─ PlayerController.cs
│
├─ ScriptableObjects
│  └─ Stage
│     └─ StageConfig.cs
│
├─ Singleton
│  ├─ BGMVolumeController.cs
│  ├─ Singleton.cs
│  └─ SoundEffect.cs
│
├─ Spawner
│  ├─ Player
│  │  └─ PlayerSpawner.cs
│  └─ Stage
│     └─ StageSpawner.cs
│
├─ Stats
│  ├─ ScriptableStats.cs
│  └─ StatHandler.cs
│
├─ UI
│  ├─ Billboard
│  │  └─ Billboard.cs
│  ├─ Effect
│  │  └─ HoverEffect.cs
│  ├─ Game
│  │  ├─ GameClearUI.cs
│  │  ├─ GameOverSkipButton.cs
│  │  ├─ GameOverTimeline.cs
│  │  ├─ GameOverUI.cs
│  │  ├─ GameUI.cs
│  │  ├─ MoveArrow.cs
│  │  └─ MoveArrow1.cs
│  ├─ Handle
│  │  └─ HandleAnimationController.cs
│  ├─ Intro
│  │  ├─ IntroSkipButton.cs
│  │  ├─ IntroTimeline.cs
│  │  └─ IntroUI.cs
│  ├─ Stage
│  │  └─ StageSelectUI.cs
│  └─ Util
│     ├─ OptionUI.cs
│     ├─ PauseUI.cs
│     └─ TimeUPUI.cs
│
└─ Zombie
   ├─ Charging
   │  └─ ChargingZombie.cs
   └─ FollowerZombie
      ├─ FollowerZombie.cs
      └─ StateMachine
         ├─ ZombieAttackState.cs
         ├─ ZombieBaseState.cs
         ├─ ZombieChargeState.cs
         ├─ ZombieChasingState.cs
         ├─ ZombieDeathState.cs
         ├─ ZombieFollowState.cs
         ├─ ZombieIdleState.cs
         ├─ ZombieRiseState.cs
         └─ ZombieStateMachine.cs
```

  



