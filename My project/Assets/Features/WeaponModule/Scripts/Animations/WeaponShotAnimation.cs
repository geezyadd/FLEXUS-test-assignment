using Features.AnimationModule.Scripts;
using Features.WeaponModule.Scripts.Turret;
using UnityEngine;

namespace Features.WeaponModule.Scripts.Animations
{
    public class WeaponShotAnimation : MonoBehaviour
    {
        private static readonly int ShootTrigger = Animator.StringToHash("Shoot");

        [SerializeField] private TurretWeapon _weapon;
        [SerializeField] private AnimationController _animationController;

        private void OnEnable() =>
            _weapon.Fired += OnFired;

        private void OnDisable()
        {
            _weapon.Fired -= OnFired;
            _animationController.ResetTrigger(ShootTrigger);
        }

        private void OnFired() =>
            _animationController.SetTrigger(ShootTrigger);
    }
}
