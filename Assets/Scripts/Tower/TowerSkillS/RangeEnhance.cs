using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Tower/Skill/RangeEnhance",fileName ="RangeEnhance")]
public class RangeEnhance : TowerSkillSO
{
    [SerializeField] Vector2Int[] range;
    public override void AffectWhenLearned(Tower tower)
    {
        tower.defaultRange.AddRange(range);
        tower.ConvertToRangeInWorld();
        tower.PromptRange();
    }
}
