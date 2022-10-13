using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    BattleManager battleManager;
    SlotGroup slotGroup;
    UnitInfo unitInfo;

    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] GameObject scorePanel;
    public List<GameObject> stars;

    private void Awake()
    {
        battleManager=FindObjectOfType<BattleManager>();
        slotGroup=FindObjectOfType<SlotGroup>();
        unitInfo=FindObjectOfType<UnitInfo>();
    }

    public void SelectTower(int index)
    {
        battleManager.SelectTower(index);
    }

    public void GetTowerSIcon(List<BuildingSO> buildingSOList)
    {
        slotGroup.InitTowerSlot(buildingSOList);
    }

    public void EnterSellMode()
    {
        battleManager.EnterSellAction();
    }

    public void ExitSellMode()
    {
        battleManager.ExitSellAction();
    }

    public void UpdateGold(int gold)
    {
        goldText.text = gold.ToString();
    }

    public void UpdateHp(int hp)
    {
        hpText.text = hp.ToString();
    }

    public void UpdateUnitInfo(Tower tower)
    {
        unitInfo.InitSkillSlots(tower);
        unitInfo.UpdateTowerInfo(tower);
    }

    public void BackToSelectScene()
    {
        GameManager.instance.LoadSelectScene();
    }

    public void EnableScorePanel(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            stars[i].gameObject.SetActive(true);
        }
        scorePanel.SetActive(true);
    }
}

