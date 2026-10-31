using System.Collections.Generic;
using Features.ScreenShakeModule.Scripts.Exceptions;
using UnityEngine;

namespace Features.ScreenShakeModule.Scripts
{
    [CreateAssetMenu(fileName = nameof(ScreenShakeConfiguration), menuName = "Screen Shake/Screen Shake Configuration")]
    public class ScreenShakeConfiguration : ScriptableObject
    {
        [SerializeField] private List<ScreenShakeSettings> _shakes = new();

        public ScreenShakeSettings GetSettings(ScreenShakeType type)
        {
            foreach (ScreenShakeSettings settings in _shakes)
            {
                if (settings.Type == type)
                    return settings;
            }

            throw new ScreenShakeNotConfiguredException(type);
        }
    }
}
