using UnityEngine;

namespace Features.UIModule.PowerWindow.Scripts
{
    [CreateAssetMenu(fileName = nameof(PowerWindowConfiguration), menuName = "UI/Power Window Configuration")]
    public class PowerWindowConfiguration : ScriptableObject
    {
        [SerializeField] private PowerView _powerViewPrefab;

        public PowerView PowerViewPrefab => _powerViewPrefab;
    }
}
