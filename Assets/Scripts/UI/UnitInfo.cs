using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    [SerializeField] UIManager uIManager;

    [Header("Unit Info")]
    [SerializeField] Image towerIcon;
    [SerializeField] List<SkillSlot> slots;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI damageTypeText;
    [SerializeField] TextMeshProUGUI attackSpeedText;
    [SerializeField] TextMeshProUGUI costText;

    private void Start()
    {
        ResetInfo();
    }


    public void InitSkillSlots(Tower tower)
    {
        if (tower == null)
        {
            foreach (var slot in slots)
            {
                slot.ResetSlot();
            }
            ResetInfo();
        }
        else
        {

            for (int i = 0; i < slots.Count; i++)
            {
                Sprite icon = null;
                bool outOfIcon = true;
                if (i < tower.towerSO.towerSkills.Count)
                {
                    icon = tower.towerSO.towerSkills[i].icon;
                    outOfIcon = false;
                }
                slots[i].InitSlot(outOfIcon, icon);
            }
        }
  
    }

    public void ResetInfo()
    {
        nameText.text = "";
        levelText.text = "";
        damageText.text = "";
        damageTypeText.text = "";
        attackSpeedText.text = "";
        costText.text = "";
        towerIcon.sprite = null;
        towerIcon.enabled = false;
    }

    public void UpdateTowerInfo(Tower tower)
    {
        if (tower == null)
        {
            ResetInfo();
            return;
        }
        nameText.text = tower.GetTowerName();
        levelText.text = tower.GetLevel();
        damageText.text = tower.GetTowerDamage();
        damageTypeText.text = tower.GetTowerDamageType();
        attackSpeedText.text = tower.GetTowerAttackInterval();
        costText.text = tower.GetCost();
        towerIcon.enabled = true;
        towerIcon.sprite = tower.towerSO.icon;
    }
}
