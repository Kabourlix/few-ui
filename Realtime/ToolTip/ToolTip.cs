using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Aurore.FewUI
{
    //[ExecuteInEditMode()]
    [AddComponentMenu("")]
    public class ToolTip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headerField;
        [SerializeField] private TextMeshProUGUI contentField;
        [SerializeField] private bool isTouch = false;
        private LayoutElement _layoutElement;

        [SerializeField] private int characterWrapLimit;

        [SerializeField] private bool lockTooltipOnSpawn = false;
        
        private RectTransform _rectTransform;

        #region Set texts
        

        public void Set(string header, string content)
        {
            SetHeader(header);
            SetContent(content);
        }
        public void SetHeader(string header) => headerField.text = header;
        public void SetContent(string content) => contentField.text = content;

        #endregion

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _layoutElement = GetComponent<LayoutElement>();
        }

        private void Update()
        {
            EnableLayout();
            if (lockTooltipOnSpawn) return;
            SetPosition();
        }

        private void EnableLayout()
        {
            _layoutElement.enabled = (headerField.text.Length > characterWrapLimit || contentField.text.Length > characterWrapLimit);
        }

        public void SetPosition()
        {
            if(Touch.activeTouches.Count == 0 && !Mouse.current.leftButton.isPressed) return;
            var mousePos = isTouch ? Touch.activeTouches[0].screenPosition : Mouse.current.position.ReadValue();
            //mousePos.z = Camera.main.nearClipPlane;

            var pivotX = mousePos.x / Screen.width;
            var pivotY = mousePos.y / Screen.height;

            _rectTransform.pivot = new Vector2(pivotX, pivotY);
            transform.position = (Vector3)mousePos + Camera.main.nearClipPlane * Vector3.forward;

        }

    }
}

