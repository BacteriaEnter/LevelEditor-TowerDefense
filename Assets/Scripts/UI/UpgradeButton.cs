using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour, IPointerClickHandler
{
    Image upgradeButton;
    [SerializeField] ButtonActionChannel buttonActionChannel;
    UIManager uiManager;
    bool canUpgrade;
    [SerializeField] Animator animator;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canUpgrade)
        {
            animator.Play("Selected");
            buttonActionChannel.UpgradeButtonClicked();
        }
    }

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        upgradeButton = GetComponent<Image>();
    }

    private void Start()
    {
        buttonActionChannel.OnUpgradeStatusEnabled += EnableUpgradeFunction;
        buttonActionChannel.onUpgradeStatusDisabled += DisableUpgradeFunction;
        DisableUpgradeFunction();
    }

    private void OnDisable()
    {
        buttonActionChannel.OnUpgradeStatusEnabled -= EnableUpgradeFunction;
        buttonActionChannel.onUpgradeStatusDisabled -= DisableUpgradeFunction;
    }

    public void EnableUpgradeFunction()
    {
        canUpgrade = true;
        upgradeButton.color = Color.white;
    }

    public void DisableUpgradeFunction()
    {
        canUpgrade = false;
        upgradeButton.color = Color.grey;
    }
}
