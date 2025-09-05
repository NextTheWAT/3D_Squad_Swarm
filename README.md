3D_Squad_Swarm  
  
작은 맵에서 좀비 vs NPC vs 플레이어가 빠르게 얽히는 3D 액션.   
상태머신(FSM)으로 캐릭터 로직을 모듈화하고, 스테이지/스폰/UI/오디오를 매니저 계층에서 제어.    
   
✨ 핵심 특징     
상태머신 아키텍처: Player / NPC / Zombie 모두 공통 FSM 패턴(StateMachine.cs) 적용       
NavMesh AI: 추적·배회·도망·차지 등 상태 기반 이동·전투    
Stage 시스템: StageConfig(SO) + StageManager/StageSpawner로 난이도·스폰·타이머 관리     
UI 파이프라인: UIManager가 인트로→선택→게임→일시정지→클리어/오버/타임업 전환    
오디오 & 카메라: AudioManager, BGMVolumeController, CameraManager, 사망/가림 이펙트 포함   
  
🧱 실제 프로젝트 구조 (Assets/02. Scripts) 

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

🔌 모듈 개요   
Managers  
GameManager : 게임 전역(일시정지 등)   
StageManager : StageConfig 로드/진행, 스폰 제어   
UIManager : UI 상태 전환(Intro/StageSelect/Game/Pause/GameOver/GameClear/TimeUP)  
AudioManager, BGMVolumeController, SoundEffect : BGM/SFX  
CameraManager + CameraDeathEffect/CameraOcclusion  
PlayerManager, ZombieManager  
  
FSM  
공통 베이스: StateMachine.cs  
Player: Idle/Walk/Ground/Attack/Death  
NPC: Idle/Ground/Flee/Chase/Attack/Death   
Zombie(Follower/Charging): Idle/Follow/Chasing/Charge/Attack/Death/Rise   
Combat/Util  
IDamageable, Bullet, GunAimer, ForceReciever, AnimationEventForwarder*  
   
Data  
ScriptableStats + StatHandler (플레이어/좀비 스탯), StageConfig(스테이지 설정)  
  



