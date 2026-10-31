namespace Features.ScreenShakeModule.Scripts
{
    public class ActiveScreenShake
    {
        public ActiveScreenShake(ScreenShakeSettings settings, float strength)
        {
            Settings = settings;
            Strength = strength;
            TimeLeft = settings.Duration;
        }

        public ScreenShakeSettings Settings { get; }
        public float Strength { get; }
        public float TimeLeft { get; private set; }
        public float Amplitude => Settings.Amplitude * Strength * TimeLeft / Settings.Duration;

        public void SetTimeLeft(float timeLeft) =>
            TimeLeft = timeLeft;
    }
}
