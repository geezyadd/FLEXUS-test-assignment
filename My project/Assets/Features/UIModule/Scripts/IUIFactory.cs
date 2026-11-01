using UnityEngine;

namespace Features.UIModule.Scripts
{
    public interface IUIFactory
    {
        TView Create<TView>(TView prefab) where TView : Component;
    }
}
