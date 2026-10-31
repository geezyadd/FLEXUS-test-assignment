namespace Features.ScreenShakeModule.Scripts
{
    public interface IScreenShakeService
    {
        void Shake(ScreenShakeType type);
        void Shake(ScreenShakeType type, float strength);
    }
}
