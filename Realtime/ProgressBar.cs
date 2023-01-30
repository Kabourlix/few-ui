using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Aurore.FewUI
{
    [RequireComponent(typeof(Slider))]
    public class ProgressBar : MonoBehaviour
    {
        private Slider _slider;
        [Range(0f,1f)]
        [SerializeField] private float sliderIncrement = 0.3f; //in percentage per seconds

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void Start()
        {
            Increment(0.75f);
        }

        private Coroutine _running;
        public void Increment(float newValue)
        {
            if (newValue is < 0 or > 1) //To display warning
            {
                Debug.LogWarning("newValue must be set between 0 and 1. The value has been clamped.");
            }
            if(_running is not null)StopCoroutine(_running);
            _running = StartCoroutine(SliderAnimate(Mathf.Clamp01(newValue)));
        }

        private IEnumerator SliderAnimate(float target)
        {
            while (Mathf.Abs(_slider.value-target) > 0.01f)
            {
                _slider.value += sliderIncrement*Time.deltaTime;
                yield return null;
            }

            _slider.value = target;
        }
    }

}
