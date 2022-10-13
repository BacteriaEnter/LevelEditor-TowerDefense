using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SubscribeButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onSlotSelected;//注册该标签选择时的事件

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSlotSelected();
    }

    public void OnSlotSelected()
    {
        onSlotSelected.Invoke();
    }
}
