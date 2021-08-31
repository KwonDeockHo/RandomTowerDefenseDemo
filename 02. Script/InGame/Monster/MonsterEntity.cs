using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    None,
    Idle,
    Move,
    Attack,
    Dead,
    Size,
}
public enum MonsterType
{
    Normal, Boss
};

public class MonsterEntity : MonoBehaviour
{
    //스텟
    [Header("Properties")]
    //레벨
    public int level = 1;
    //public CardDataSet Card;

    public MonsterType team = MonsterType.Normal;

    new public Collider collider;
    public NavMeshAgent agent;
    public Animator animator;
    public State state;
    protected bool isRun = false;

    public MonsterEntity target;

    [Header("Skills")]
    public SkillTemplate[] skillTemplates;
    public List<Skill> skills = new List<Skill>();
    protected int currentSkill = -1;

    public static Dictionary<MonsterType, HashSet<MonsterEntity>> monsterTypes = new Dictionary<MonsterType, HashSet<MonsterEntity>>() {
        {MonsterType.Normal, new HashSet<MonsterEntity>()},
        {MonsterType.Boss, new HashSet<MonsterEntity>()}

    };


    //공격력
    protected LevelBasedInt _damage = new LevelBasedInt { baseValue = 1 };
    public virtual int damage
    {
        get
        {
            return _damage.Get(level);
        }
    }

    //최대 체력
    protected LevelBasedInt _healthMax = new LevelBasedInt { baseValue = 100 };
    public virtual int healthMax
    {
        get
        {
            return _healthMax.Get(level);
        }
    }
    //체력 재생
    protected LevelBasedInt _healthRegeneration = new LevelBasedInt { baseValue = 1 };
    public virtual int healthRegeneration
    {
        get
        {
            return _healthRegeneration.Get(level);
        }
    }
    //현재 체력
    int _health = 1;
    public int health
    {
        get { return Mathf.Min(_health, healthMax); }
        set { _health = value; }
    }

    //최대 마나
    protected LevelBasedInt _manaMax = new LevelBasedInt { baseValue = 100 };
    public virtual int manaMax
    {
        get
        {
            return _manaMax.Get(level);
        }
    }
    //마나 재생
    protected LevelBasedInt _manaRegeneration = new LevelBasedInt { baseValue = 1 };
    public virtual int manaRegeneration
    {
        get
        {
            return _manaRegeneration.Get(level);
        }
    }
    //현재 마나
    int _mana = 1;
    public int mana
    {
        get { return Mathf.Min(_mana, manaMax); }
        set { _mana = value; }
    }

    //방어력
    protected LevelBasedInt _armor = new LevelBasedInt { baseValue = 1 };
    public virtual int armor
    {
        get
        {
            return _armor.Get(level);
        }
    }

    //크리티컬 확률(1 => 100%)
    protected LevelBasedFloat _criticalChance = new LevelBasedFloat { baseValue = 0 };
    public virtual float criticalChance
    {
        get
        {
            return _criticalChance.Get(level);
        }
    }

    //크리티컬 데미지(배율 1 => 100%)
    protected LevelBasedFloat _criticalDamage = new LevelBasedFloat { baseValue = 2 };
    public virtual float criticalDamage
    {
        get
        {
            return _criticalDamage.Get(level);
        }
    }

    //이동속도
    protected LevelBasedFloat _moveSpeed = new LevelBasedFloat { baseValue = 10 };
    public virtual float moveSpeed
    {
        get
        {
            return _moveSpeed.Get(level);
        }
    }

    //공격속도(배율 1 => 100%)
    protected LevelBasedFloat _attackSpeed = new LevelBasedFloat { baseValue = 1 };
    public virtual float attackSpeed
    {
        get
        {
            return _attackSpeed.Get(level);
        }
    }

    //쿨감(배율 1 => 100%)
    protected LevelBasedFloat _cooldown = new LevelBasedFloat { baseValue = 1 };
    public virtual float cooldown
    {
        get
        {
            return _cooldown.Get(level);
        }
    }

    //피흡(배율 1 => 100%)
    protected LevelBasedFloat _absorption = new LevelBasedFloat { baseValue = 0 };
    public virtual float absorption
    {
        get
        {
            return _absorption.Get(level);
        }
    }

    //쉴드
    int _shield = 0;
    public int shield
    {
        get { return _shield; }
        set { _shield = value; }
    }

    public bool IsMoving()
    {
        return agent.pathPending ||
               agent.remainingDistance > agent.stoppingDistance ||
               agent.velocity != Vector3.zero;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        monsterTypes[team].Add(this);

        if (!agent)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed;
            agent.acceleration = moveSpeed * 100;
        }
        if (!animator) animator = GetComponent<Animator>();
        if (!collider) collider = GetComponent<Collider>();

        state = State.Idle;

        //foreach (var t in skillTemplates)
        //    skills.Add(new Skill(t));

        health = healthMax;
    }

    public virtual void UpdateState()
    {
        if (state == State.Idle) UpdateState_Idle();
        else if (state == State.Move) UpdateState_Move();
        else if (state == State.Attack) UpdateState_Attck();
        else if (state == State.Dead) UpdateState_Dead();
    }

    public virtual void UpdateState_Idle()
    {

    }

    public virtual void UpdateState_Move()
    {
        agent.speed = moveSpeed;
        agent.acceleration = moveSpeed * 100;
        if ((state == State.Move) && !IsMoving())
        {
            state = State.Idle;
        }
    }

    public virtual void UpdateState_Attck()
    {

    }

    public virtual void UpdateState_Dead()
    {

    }

    public virtual bool CanAttack(MonsterEntity _target)
    {
        return true;
    }

    public virtual void DealDamageAt(MonsterEntity _target, int amount)
    {
        int damageDealt = amount;
        _target.health -= damageDealt;
    }

    public virtual void MoveTo(Vector3 destination, float stopDistance = 0)
    {
        agent.destination = destination;
        agent.stoppingDistance = stopDistance;
        state = State.Move;
    }

    void OnDestroy()
    {
        monsterTypes[team].Remove(this);
    }

    public void StopAction()
    {
        state = State.Idle;
        agent.destination = transform.position;
    }

    //public bool CastCheckSelf(Skill skill)
    //{
    //    //쿨돌앗나, 살아있나, 마나 남았나
    //    return (skill.IsReady()) &&
    //           health > 0 &&
    //           mana >= skill.manaCosts;
    //}

    //public bool CastCheckTarget(Skill skill)
    //{
    //    //스킬쓸수있는 타겟인가
    //    return skill.CheckTarget(this);
    //}

    //public bool CastCheckDistance(Skill skill, out Vector3 destination)
    //{
    //    //스킬쓸수있는거리인가
    //    return skill.CheckDistance(this, out destination);
    //}

    //public void CastSkill(Skill skill)
    //{
    //    if (CastCheckSelf(skill) && CastCheckTarget(skill))
    //    {
    //        skill.Apply(this);
    //        mana -= skill.manaCosts;
    //        skill.cooldownEnd = Time.time + skill.cooldown;
    //        skills[currentSkill] = skill;
    //    }
    //    else
    //    {
    //        currentSkill = -1;
    //    }
    //}

}
