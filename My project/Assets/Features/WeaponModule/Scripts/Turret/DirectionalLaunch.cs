using UnityEngine;

namespace Features.WeaponModule.Scripts.Turret
{
    public class DirectionalLaunch : LaunchVelocityProviderBase
    {
        [SerializeField] private ShotPowerRegulator _powerRegulator;

        public override Vector3 GetLaunchVelocity() =>
            transform.forward * _powerRegulator.Power;
    }
}
