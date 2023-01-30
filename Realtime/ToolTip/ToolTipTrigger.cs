using System;
using System.Collections;
using System.Collections.Generic;
using Aurore.FewUI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string header;
    [SerializeField][TextArea] private string content;
    
    [Range(0.2f,2f)]
    [Tooltip("Specify the delay time in seconds before the tooltip to be shown")]
    [SerializeField] private float delayTime = 0.2f;
    private WaitForSeconds _delayWait;

    private Coroutine _current;
    private void Awake()
    {
        _delayWait = new WaitForSeconds(delayTime);
    }

    private IEnumerator Display()
    {
        yield return _delayWait;
        ToolTipSystem.Instance.Show(header, content);
        _current = null;
    }
    
    
    #region For Ui tooltip

    public void OnPointerEnter(PointerEventData eventData)
    {
        _current = StartCoroutine(Display());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_current is not null) StopCoroutine(_current);
        ToolTipSystem.Instance.Hide();
    }

    #endregion
    
    #region For gameobject Tooltip

    private void OnMouseEnter()
    {
        _current = StartCoroutine(Display());
    }

    private void OnMouseExit()
    {
        if(_current is not null) StopCoroutine(_current);
        ToolTipSystem.Instance.Hide();
    }

    #endregion
}
