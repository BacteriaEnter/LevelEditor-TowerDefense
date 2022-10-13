using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellButton : MonoBehaviour, IPointerClickHandler
{
    Image sellButton;
    [SerializeField] bool isSelected;
    [SerializeField] ButtonActionChannel buttonActionChannel;
    [SerializeField] Sprite idleSprite;
    [SerializeField] Sprite selectedSprite;

    UIManager uiManager;



    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        sellButton=GetComponent<Image>();
       
    }

    private void Start()
    {
        buttonActionChannel.OnSellButtonClicked += ExitSellAction;
    }

    private void OnDisable()
    {
        buttonActionChannel.OnSellButtonClicked -= ExitSellAction;
    }

    public void EnterSellMode()
    {
        isSelected = true;
        sellButton.sprite = selectedSprite;
        uiManager.EnterSellMode();
    }

    public void ExitSellMode()
    {
        isSelected = false;
        sellButton.sprite = idleSprite;
        uiManager.ExitSellMode();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSelected)
        {
            EnterSellMode();
        }
        else
        {
            ExitSellMode();
        }
    }

    private void ExitSellAction()
    {
        isSelected = false;
        sellButton.sprite = idleSprite;
    }
}
