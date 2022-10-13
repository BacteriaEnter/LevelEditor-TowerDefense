using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Tower/Skill/Rally", fileName = "Rally")]
public class RallySkill : TowerSkillSO
{
    [SerializeField] Effect rally;

    public override void AffectWhenLearned(Tower tower)
    {
        tower.AddPersistentEffect(rally);
    }
}
