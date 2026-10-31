using UnityEngine;

namespace Features.WeaponModule.Scripts.Turret
{
    public abstract class LaunchVelocityProviderBase : MonoBehaviour
    {
        public abstract Vector3 GetLaunchVelocity();
    }
}
