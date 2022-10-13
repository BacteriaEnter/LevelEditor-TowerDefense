using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ButtonActionChannel", fileName = "ButtonActionChannel")]
public class ButtonActionChannel : ScriptableObject
{
    public UnityAction OnSellButtonClicked;
    public UnityAction OnUpgradeButtonClicked;
    public UnityAction OnUpgradeStatusEnabled;//可以使用升级按钮时
    public UnityAction onUpgradeStatusDisabled;//禁用升级按钮时


    public void ExitSellActionByChannel()
    {
        OnSellButtonClicked?.Invoke();
    }

    public void UpgradeButtonClicked()
    {
        OnUpgradeButtonClicked?.Invoke();
    }

    public void UpgradeStatusEnable()
    {
        OnUpgradeStatusEnabled?.Invoke();
    }

    public void UpgradeStatusDisable()
    {
        onUpgradeStatusDisabled?.Invoke();
    }
}
