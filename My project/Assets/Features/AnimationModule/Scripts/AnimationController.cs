using UnityEngine;

namespace Features.AnimationModule.Scripts
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void SetTrigger(int triggerHash) =>
            _animator.SetTrigger(triggerHash);

        public void ResetTrigger(int triggerHash) =>
            _animator.ResetTrigger(triggerHash);
    }
}
