namespace Features.UIModule.PowerWindow.Scripts
{
    public interface IPowerView
    {
        void SetRange(float min, float max);
        void SetPower(float power);
        void SetVisible(bool isVisible);
    }
}
