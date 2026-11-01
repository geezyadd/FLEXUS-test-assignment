FLEXUS-test-assignment
======================

Cannon with custom (non-Rigidbody) projectile physics, ricochets, procedural projectile meshes,
trajectory preview and player feedback: Render Texture impact decals, explosion on the last ricochet,
camera shake and barrel recoil.


REQUIREMENTS COVERAGE
---------------------

- Cannon control
    Yaw and barrel pitch with the mouse, shot power with the wheel (smoothed).
- Trajectory preview, adjustable shot power / projectile speed
    TrajectoryModule - predicted with the same physics step as real projectiles.
- Procedural projectile mesh, different shapes
    MeshGenerationModule - cube with randomly offset corners, flat shading.
- Custom physics without Rigidbody / Physics.Simulate
    PhysicsModule - own integration, forces, drag, shape casts, smoothing.
- Ricochet
    CollisionSolver + CollisionMaterial (restitution / friction).
- Impact mark via Render Texture
    ImpactDecalModule - decals are stamped into a per-surface Render Texture.
- Explosion on the last ricochet
    Ricochet limit in WeaponConfiguration; VFX + sound + camera shake.
- Camera shake
    ScreenShakeModule (Cinemachine Perlin noise).
- Barrel recoil animation
    Animator trigger on fire (AnimationModule).

Extra feedback: power UI, projectile trail, squash on bounce, shot / ricochet / explosion sounds,
cursor lock.


HOW TO RUN
----------

- Unity 6000.0.67f1, URP 17.0.4
- Packages: Addressables 2.8.1, Cinemachine 3.1.7, Input System 1.18.0, Visual Effect Graph 17.0.4,
  Extenject (Zenject) in Assets/Plugins
- Open "My project" and start from Assets/Scenes/BootstrapScene.
  BootstrapScene -> GlobalScene (single) -> GameScene (additive).

Controls:
  Mouse               rotate turret / raise and lower barrel
  Mouse wheel         shot power
  Left mouse button   fire; also locks the cursor
  Esc                 unlock the cursor


ARCHITECTURE
------------

- Zenject for composition. Each feature is a module (own assembly definition) with an installer.
  Contexts: Project (assets, scenes, input) -> Global (configs, models, game updater, audio, cursor)
  -> Game (physics, weapon, camera, VFX, decals, UI ...).
- Configurations are ScriptableObjects loaded through Addressables (Address constants are generated).
- Models hold data only, systems run the per-frame logic through a single IGameUpdater,
  services expose an API to other code. Logic depends on interfaces.
- Weapon features are capabilities (IRotatable, IElevatable, IPowerAdjustable) requested through
  IWeaponService.TryGetCapability<T>(), so new weapons and features plug in without changing the service.
- Feedback is attached as small components subscribed to events (Fired, Ricocheted, Exploded),
  so the weapon and the projectile know nothing about audio, VFX or shake.

Modules:
  PhysicsModule         bodies, gravity, drag, box/sphere colliders via shape casts, ricochet, render smoothing
  WeaponModule          turret, shooting, power, projectile, projectile pool, feedback components
  MeshGenerationModule  random cube mesh per projectile
  TrajectoryModule      trajectory prediction (shared PhysicsSimulator step) and line rendering
  ImpactDecalModule     Render Texture decals: stamp shader + per-surface canvas
  VfxModule             pooled effects by type, Play / Attach (trail follows the projectile),
                        adapters for VFX Graph and Particle System
  AudioModule           IAudioService.Play(type, position) with a pooled AudioSource implementation
                        (FMOD can be swapped in)
  ScreenShakeModule     shake by type and strength, strongest active shake wins
  CameraModule          Cinemachine cameras by type, spawned from config
  AnimationModule       Animator parameter wrapper
  UIModule              UI factory; power window (MVP) in its own assembly
  PoolModule            generic PoolBase<T> used by projectiles, VFX and audio sources
  InputModule           Input System actions
  CursorModule          cursor lock / unlock
  AssetLoaderModule     Addressables loading
  SceneLoaderModule     scene flow
  GameUpdaterModule     single update loop


KEY DECISIONS
-------------

- Custom physics. Fixed-step integration with forces and drag; collisions are resolved with
  BoxCast / SphereCast against static Unity colliders (queries only, no Rigidbody). The impact point is
  the projection of the projectile centre onto the surface, so feedback appears exactly under it.
- Trajectory uses the same PhysicsSimulator step and projectile collider casts as the real flight,
  and is recomputed only when the aim or power changes.
- Decals. Each surface owns a Render Texture sized by its area (pixels per metre). On hit the surface
  UV is found by a raycast on its MeshCollider, and a stamp shader blits the decal into the texture.
  The surface Shader Graph blends it over the base colour. Cost per frame does not grow with the number
  of decals.
- Pools for projectiles, effects and audio sources - no Instantiate / Destroy while shooting.
  The trail lives outside the projectile so it can finish when the projectile returns to its pool.
