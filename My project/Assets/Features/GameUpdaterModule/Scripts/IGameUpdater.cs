using System;

namespace Features.GameUpdaterModule.Scripts
{
    public interface IGameUpdater
    {
        event Action<float> Updated;
        event Action<float> FixedUpdated;
        event Action<float> LateUpdated;
    }
}
