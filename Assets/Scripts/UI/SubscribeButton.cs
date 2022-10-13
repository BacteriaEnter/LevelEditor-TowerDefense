using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SubscribeButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onSlotSelected;//ע��ñ�ǩѡ��ʱ���¼�

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSlotSelected();
    }

    public void OnSlotSelected()
    {
        onSlotSelected.Invoke();
    }
}
