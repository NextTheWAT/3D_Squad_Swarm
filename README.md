# 3D_Squad_Swarm  

<img width="1024" height="1024" alt="GameTitle_SquadSwarm" src="https://github.com/user-attachments/assets/7086f5f8-9d51-44a3-88ce-c603589444ca" />

## 🎮 게임소개
작은 맵에서 플레이어(좀비) vs NPC 가 빠르게 충돌하는 라운드형 3D 액션입니다.  
모든 캐릭터는 상태머신(FSM) 으로 움직이며, 스테이지 설정(SO) 과 스폰 규칙으로 라운드 난이도를 제어합니다.  
UI는 인트로 → 스테이지선택 → 게임 → 일시정지/클리어/게임오버/타임업으로 전환됩니다.  

제한시간(Timer)과 감염도 게이지가 핵심 규칙  
 
적 처치 시 감염도 상승 → GameClear  
  
시간 초과 → TimeUP  

플레이어 사망 → GameOver  


## ▶️ 플레이영상
([https://your.video.link](https://youtu.be/6wKTC_5yyMQ))  

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

---

# 게임 기능 구현 내용

<aside>

## **주인공 캐릭터의 이동 및 기본 동작**
</aside>

![주인공 캐릭터의 이동 및 기본 동작](https://github.com/user-attachments/assets/1f64027a-e419-4ecd-a8dd-f8da966df563)

<aside>

## **레벨 디자인 및 적절한 게임 오브젝트 배치**
</aside>

![레벨 디자인 및 적절한 게임 오브젝트 배치1](https://github.com/user-attachments/assets/dac3f5f4-d099-48a6-aaed-8e5230600221)

<aside>

## **충돌 처리 및 피해량 계산**
</aside>

![ezgif com-cut](https://github.com/user-attachments/assets/c51f5790-74df-4a57-a024-cc233a385d94)

<aside>

## **UI/UX 요소**
</aside>

### (Home 메뉴)
![HomeScene-ezgif com-cut](https://github.com/user-attachments/assets/6817f0a6-5a72-4d5b-98ab-41310b490244)

### (In Game UI)
![GameUI](https://github.com/user-attachments/assets/b15e05ce-88ee-4abf-b200-e53561cc0919)

<aside>

## **다양한 적 캐릭터와 그들의 행동 패턴 추가**
</aside>

![다양한 적 캐릭터와 그들의 행동 패턴 추가](https://github.com/user-attachments/assets/49b260cd-995b-4e00-8c03-608a8cd71e2c)

<aside>

## **다양한 환경과 배경 설정**
</aside>

### (Stage1 - 마을/낮)

<img width="1920" height="1080" alt="Stage1" src="https://github.com/user-attachments/assets/57dc608f-ccf2-4e2e-bdcd-9f3dd05966b0" />

### (Stage2 - 마을/밤,안개)

<img width="1920" height="1080" alt="Stage2" src="https://github.com/user-attachments/assets/e56c061f-a7f3-46dc-902b-41c3ae80bced" />

### (Stage3 - 숲)

<img width="1920" height="1080" alt="Stage3" src="https://github.com/user-attachments/assets/0e56572b-0e35-4c3e-8699-c594dead4360" />

<aside>

## **다양한 어려움 모드 또는 난이도 설정**
</aside>

### (스테이지별 스탯 차별화)
<img width="246" height="396" alt="다양한 어려움 모드 또는 난이도 설정" src="https://github.com/user-attachments/assets/872275af-dd7a-4aef-92e6-680782046b73" />

<aside>

## **다양한 엔딩 및 스토리 브랜치**
</aside>

### (게임 인트로씬)

![IntroScene](https://github.com/user-attachments/assets/cf65d4b6-bd9b-42fe-82e6-b372fbd7b142)

### (타임오버엔딩씬)
![TimeOverScene-ezgif com-cut](https://github.com/user-attachments/assets/813b20ff-863e-48b5-84ad-9307794cafbb)

<aside>

## **경험치 및 업그레이드 시스템**
</aside>

### (VIP 시민을 사냥하면 플레이어와 좀비무리의 속도가 증가)

![경험치 및 업그레이드 시스템1](https://github.com/user-attachments/assets/98dadbc7-26f9-45fe-8ac3-1ab59366541d)

<aside>

## **AI 적 캐릭터의 인공 지능**
</aside>

![AI 적 캐릭터의 인공 지능 개선](https://github.com/user-attachments/assets/453ab88d-c56e-48ce-b2c4-ee0288f970de)

<aside>

## **특수 효과 및 파티클 시스템 추가**
</aside>

### (좀비화 효과)
![특수 효과 및 파티클 시스템 추가](https://github.com/user-attachments/assets/c0856a12-f087-408c-9ea6-d37d98bcf68c)

### (맵 안개 효과)
![특수 효과 및 파티클 시스템 추가2](https://github.com/user-attachments/assets/6fd3f695-0359-47cf-b60b-5df92a3d049a)

<aside>

 ## 🧯 Troubleshooting
<img width="950" height="227" alt="image" src="https://github.com/user-attachments/assets/4d6459ee-bcd3-4ccc-9894-9a69c8268b2c" />
<img width="950" height="246" alt="image" src="https://github.com/user-attachments/assets/2f90818a-8ab8-44f5-a8a0-7d571a9d2f8c" />
<img width="950" height="194" alt="image" src="https://github.com/user-attachments/assets/e612e5f4-f16a-4be0-a162-67e2e8cf00f1" />
<img width="950" height="255" alt="image" src="https://github.com/user-attachments/assets/d444afaf-30f2-48eb-8cc9-6eaa94ab4c33" />





