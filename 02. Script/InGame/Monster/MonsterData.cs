using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;


[CreateAssetMenu(menuName = "Monster/Monster DataSet", order = 1)]

public class MonsterData : MonsterInfo
{
    public LevelBasedInt damage = new LevelBasedInt { baseValue = 1 };

    public override bool CheckTarget(MonsterEntity caster)
    {
        return true;
        //   return caster.CardId != null;
    }

    public override bool CheckDistance(MonsterEntity caster, int skillLevel, out Vector3 destination)
    {
        if (caster != null)
        {

            destination = Vector3.zero;
            //destination = caster.gameObject.collider.ClosestPointOnBounds(caster.transform.position);
            return true;

            //return Utils.ClosestDistance(caster.collider, caster.target.collider) <= castRange.Get(skillLevel);
        }
        destination = Vector3.zero;
        return false;
    }

    public override void Apply(MonsterEntity caster, int skillLevel)
    {
        //caster.DealDamageAt(caster.target, caster.damage + damage.Get(skillLevel));
    }

    public override string ToolTip(int skillLevel, bool showRequirements = false)
    {
        StringBuilder tip = new StringBuilder(base.ToolTip(skillLevel, showRequirements));
        tip.Replace("{DAMAGE}", damage.Get(skillLevel).ToString());
        return tip.ToString();
    }
}
