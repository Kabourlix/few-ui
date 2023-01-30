using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Aurore.FewUI
{
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("")]
    public class ToolTipSystem : MonoBehaviour
    {
        public static ToolTipSystem Instance;

        private ToolTip _tooltip;

        private CanvasGroup _canvasGroup;
        [Tooltip("Alpha variation curve through the animation.")]
        [SerializeField] private AnimationCurve alphaCurve;
        
        [Tooltip("Animation length of tooltip apparition")]
        [Range(0.01f,0.5f)]
        [SerializeField] private float animTime = 0.2f;
        private void Awake()
        {
            if(Instance is not null && Instance != this) Destroy(gameObject);
            Instance = this;
            
            _tooltip = GetComponentInChildren<ToolTip>();
            if (_tooltip is null) throw new NullReferenceException("Tooltip has not been found. Check if TooltipSystem is parent of the tooltip dialog box.");
            
            _canvasGroup = GetComponent<CanvasGroup>();

            _canvasGroup.alpha = 0;
        }

        

        private Coroutine _showCoroutine;
        public void Show(string header, string content)
        {
            _tooltip.Set(header, content);
            _showCoroutine = StartCoroutine(ShowSmooth());
            //ShowToolTip(true);
        }

        private IEnumerator ShowSmooth()
        {
            _tooltip.SetPosition();
            var time = 0f;
            while(_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha = alphaCurve.Evaluate(Mathf.InverseLerp(0f,animTime,time));
                yield return null;
                time += Time.deltaTime;
            }

            _showCoroutine = null;
            
        }

        private IEnumerator HideSmooth()
        {
            var time = animTime;
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha = alphaCurve.Evaluate(Mathf.InverseLerp(0f,animTime, time));
                yield return null;
                time -= Time.deltaTime;
            }
        }

        public void Hide()
        {
            if(_showCoroutine is not null) StopCoroutine(_showCoroutine);
            StartCoroutine(HideSmooth());
        }
    }
}
