using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;

[CreateAssetMenu(menuName = "Monster/Monster DataSet", order = 2)]
public class Monster : MonsterEntity
{
    private void Update()
    {
        UpdateState();
    }

    public override void UpdateState_Idle()
    {
        if (!target)
        {
            float dist = 100000;
            foreach (var player in MonsterEntity.monsterTypes[MonsterType.Normal])
            {
                if (!player) break;
                float d = Vector3.Distance(player.transform.position, transform.position);
                if (dist >= d)
                {
                    dist = d;
                    target = player;
                }
            }
        }
        if (target)
        {
            MoveTo(target.transform.position, 0);
        }
    }

    public override void UpdateState_Move()
    {
        base.UpdateState_Move();
    }

    public override void UpdateState_Attck()
    {
        base.UpdateState_Attck();
    }

    public override void UpdateState_Dead()
    {
        base.UpdateState_Dead();
    }

    private void LateUpdate()
    {
        animator.SetBool("Walk", state == State.Move);

        foreach (Skill skill in skills)
            if (skill.learned)
                animator.SetBool(skill.name, skill.CastTimeRemaining() > 0);

        animator.SetBool("Dead", state == State.Dead);
    }

    public override void MoveTo(Vector3 destination, float stopDistance = 0)
    {
        base.MoveTo(destination, stopDistance);
    }
}
