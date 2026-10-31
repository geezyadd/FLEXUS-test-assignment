using UnityEngine;

namespace Features.ScreenShakeModule.Scripts
{
    public class ScreenShakeService : IScreenShakeService
    {
        private readonly ScreenShakeModel _model;
        private readonly ScreenShakeConfiguration _configuration;

        public ScreenShakeService(ScreenShakeModel model, ScreenShakeConfiguration configuration)
        {
            _model = model;
            _configuration = configuration;
        }

        public void Shake(ScreenShakeType type) =>
            Shake(type, 1f);

        public void Shake(ScreenShakeType type, float strength) =>
            _model.AddShake(_configuration.GetSettings(type), Mathf.Max(0f, strength));
    }
}
