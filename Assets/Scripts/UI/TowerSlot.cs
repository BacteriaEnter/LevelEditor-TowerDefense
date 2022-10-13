using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] SlotGroup slotGroup;
    [SerializeField] int slotIndex;
    [SerializeField] Sprite icon;
    [SerializeField] Image image;
    public UnityEvent onSlotSelected;//ע��ñ�ǩѡ��ʱ���¼�
    public UnityEvent onSlotDeselected;//ע��ñ�ǩѡ��ʱ���¼�

    private void Awake()
    {
        slotGroup = GetComponentInParent<SlotGroup>();
        //image = GetComponentInChildren<Image>();
    }


    public void InitSlot(bool outOfRange,Sprite icon)
    {
        if (outOfRange)
        {
            //Disable Icon;
        }
        else
        {
            image.sprite = icon;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        slotGroup.SelectTower(slotIndex);
    }

    public void OnSlotSelected()
    {
        onSlotSelected.Invoke();
    }

    public void OnSlotDeselected()
    {
        onSlotDeselected.Invoke();
    }


}
