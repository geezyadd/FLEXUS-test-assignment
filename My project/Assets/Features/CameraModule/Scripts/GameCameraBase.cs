using UnityEngine;

namespace Features.CameraModule.Scripts
{
    public abstract class GameCameraBase : MonoBehaviour
    {
        public abstract void Activate();
        public abstract void SetTarget(Transform target);
    }
}
