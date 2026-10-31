using Unity.Cinemachine;
using UnityEngine;

namespace Features.CameraModule.Scripts
{
    public class GameCamera : GameCameraBase
    {
        [SerializeField] private CinemachineCamera _camera;

        public override void Activate() =>
            _camera.Prioritize();

        public override void SetTarget(Transform target) =>
            _camera.Target.TrackingTarget = target;
    }
}
