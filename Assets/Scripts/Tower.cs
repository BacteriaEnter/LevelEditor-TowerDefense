using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Animator anim;
    public Vector2Int towerPosition;
    public BuildingSO towerSO;
    public List<Vector2Int> rangeInWorld;
    public List<Vector2Int> defaultRange;//
    [SerializeField] TowerFacing towerFacing = TowerFacing.Left;
    public List<TowerSkillSO> skills=new List<TowerSkillSO>();
    public List<TowerSkillSO> skills_AttackTriggerList=new List<TowerSkillSO>();
    public List<TowerSkillSO> skills_CrossTriggerList=new List<TowerSkillSO>();


    [Header("Flag")]
    public bool isplaced;
    public bool readyToAttack;


    [Header("Status")]
    public float currentInterval;
    public float minDamage;
    public float maxDamage;
    public float currentTimeScale=1;
    public int currentLevel;
    public int targetCount;
    [SerializeField] float coolDownTimer;
    public DamageType damageType;

    [Header("Effect List")]
    public List<Effect> persistentEffects = new List<Effect>();
    public Dictionary<Effect, float> dic_effects = new Dictionary<Effect, float>();

    private void Awake()
    {
        Init();
    }

    public void RotateLeft()
    {
        towerFacing--;
        if (towerFacing<0)
        {
            towerFacing = TowerFacing.Bottom;
        }
        LevelManager.Instance.ResetRangePromption(rangeInWorld);
        rangeInWorld.Clear();
        foreach (var item in towerSO.range)
        {
            Vector2Int range = item;
            GetRangePromptionByFacing(ref range);
            Vector2Int pos = range + towerPosition;
            rangeInWorld.Add(pos);
        }
        LevelManager.Instance.PrompRangePosition(rangeInWorld);
    }

    public void RotateRight()
    {
        towerFacing++;
        if (towerFacing > TowerFacing.Bottom)
        {
            towerFacing = TowerFacing.Left;
        }

        LevelManager.Instance.ResetRangePromption(rangeInWorld);
        rangeInWorld.Clear();
        foreach (var item in towerSO.range)
        {
            Vector2Int range = item;
            GetRangePromptionByFacing(ref range);
            Vector2Int pos = range + towerPosition;
            rangeInWorld.Add(pos);
        }
        LevelManager.Instance.PrompRangePosition(rangeInWorld);
    }

    public void PromptRange_Select(Vector2Int mousePosition)
    {
        //向下取整的鼠标
        if (towerPosition!=mousePosition)
        {
            towerPosition = mousePosition;
            //Reset These Block 
            LevelManager.Instance.ResetRangePromption(rangeInWorld);
            rangeInWorld.Clear();
            ConvertToRangeInWorld();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="range"></param>
    public void GetRangePromptionByFacing(ref Vector2Int range)
    {
        if (towerFacing == TowerFacing.Left)
        {
            range.x = -range.x;
            range.y = -range.y;
        }
        else if (towerFacing == TowerFacing.Top)
        {
            int x = range.x;
            range.x = -range.y;
            range.y = x;
        }
        else if (towerFacing == TowerFacing.Bottom)
        {
            int x = range.x;
            range.x = range.y;
            range.y = -x;
        }
    }

    private void Update()
    {
        if (isplaced)
        {
            towerSO.TriggerSkillWhenEnemyCross(this, rangeInWorld);
            if (readyToAttack==false)
            {
                if (towerSO.CoolDown(ref coolDownTimer, currentInterval, currentTimeScale))
                {
                    readyToAttack = true;
                }
            }
            else
            {
                readyToAttack=!towerSO.Attack(this,minDamage,maxDamage,damageType,rangeInWorld);
            }            
        }
    }

    void Init()
    {
        currentInterval = towerSO.baseInterval[currentLevel];
        damageType = towerSO.DamageType;
        minDamage = towerSO.minDamage[currentLevel];
        maxDamage = towerSO.maxDamage[currentLevel];
        targetCount = towerSO.targetCount;
        defaultRange = towerSO.range.ToList();
        FindAvailableSkill();
    }

    public void PromptRange()
    {
        LevelManager.Instance.PrompRangePosition(rangeInWorld);
    }

    public void ResetRangePromption()
    {
        LevelManager.Instance.ResetRangePromption(rangeInWorld);
    }

    public void SelectedInWorld()
    {
        PromptRange();
    }



    public void Upgrade()
    {
        if (currentLevel<towerSO.maxDamage.Count)
        {
            currentLevel++;
            currentInterval = towerSO.baseInterval[currentLevel];
            damageType = towerSO.DamageType;
            minDamage = towerSO.minDamage[currentLevel];
            maxDamage = towerSO.maxDamage[currentLevel];
            FindAvailableSkill();
        }

    }


    public int SellTower()
    {
        int price=Mathf.RoundToInt(towerSO.cost[currentLevel] * 0.4f);
        ClearEffectList();
        Destroy(gameObject);
        return price;
    }

    void FindAvailableSkill()
    {
        foreach (var skill in towerSO.towerSkills)
        {
            if (currentLevel==skill.requestLevel)
            {
                skills.Add(skill);
                if (skill.attackPriority)
                {
                    skills_AttackTriggerList.Add(skill);
                }
                if (skill.crossPriority)
                {
                    skills_CrossTriggerList.Add(skill);
                }
                skill.AffectWhenLearned(this);
            }
        }
    }

    public void ConvertToRangeInWorld()
    {
        foreach (var item in defaultRange)
        {
            Vector2Int range = item;
            GetRangePromptionByFacing(ref range);
            Vector2Int pos = range + towerPosition;
            if (!rangeInWorld.Contains(pos))
            {
                rangeInWorld.Add(pos);
            }

        }
    }

    public void AddTemporaryEffect(Effect effect)
    {
        if (!dic_effects.ContainsKey(effect))
        {
            dic_effects.Add(effect,effect.affectTime);
            effect.AffectWhenEnter(this);
        }
    }

    public void AddPersistentEffect(Effect effect)
    {
        if (!persistentEffects.Contains(effect))
        {
            persistentEffects.Add(effect);
            effect.AffectWhenEnter(this);
        }
    }

    public void AffectEffectWhenTowerPlaced()
    {
        foreach (var eff in persistentEffects)
        {
            eff.AffectWhenTowerPlaced(this);
        }
    }

 

    /// <summary>
    /// 清除所有的状态
    /// </summary>
    void ClearEffectList()
    {
        foreach (var ef in dic_effects)
        {
            ef.Key.AffectWhenExit(this);
        }
        foreach (var ef in persistentEffects)
        {
            ef.AffectWhenExit(this);
        }
        dic_effects.Clear();
        persistentEffects.Clear();
    }

    /// <summary>
    /// 检测塔是否能够升级
    /// </summary>
    /// <returns></returns>
    public bool CheckUpgradeAvailable()
    {
        if (currentLevel<towerSO.cost.Count-1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region Tower Info
    public string GetTowerName()
    {
        return towerSO.towerName;
    }

    public string GetTowerDamage()
    {
        string result="";
        if (minDamage!=maxDamage)
        {
            result = $"{minDamage}~{maxDamage}"; 
       
        }
        else
        {
            result = $"{minDamage}";
        }
        return result;
    }

    public string GetTowerAttackInterval()
    {

        return $"{currentInterval}s";
    }

    public string GetTowerDamageType()
    {
        string result = "";
        if (damageType==DamageType.Physics)
        {
            result = "物理伤害";
        }
        else if (damageType == DamageType.Magic)
        {
            result = "魔法伤害";
        }
        else if (damageType == DamageType.True)
        {
            result = "真实伤害";
        }
        return result;      
    }

    public string GetCost()
    {
        string result = "";
        if (currentLevel + 1< towerSO.cost.Count)
        {
            result = towerSO.cost[currentLevel + 1].ToString();
        }
        else
        {
            result = "×";
        }
        return result;

    }

    public string GetLevel()
    {
        return $"Level {currentLevel + 1}";
    }
    #endregion
}
