using UnityEngine;
using UnityEngine.UI;

namespace Features.UIModule.PowerWindow.Scripts
{
    public class PowerView : MonoBehaviour, IPowerView
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _valueText;

        public void SetRange(float min, float max)
        {
            _slider.minValue = min;
            _slider.maxValue = max;
        }

        public void SetPower(float power)
        {
            _slider.value = power;
            _valueText.text = Mathf.RoundToInt(power).ToString();
        }

        public void SetVisible(bool isVisible) =>
            _canvas.enabled = isVisible;
    }
}
