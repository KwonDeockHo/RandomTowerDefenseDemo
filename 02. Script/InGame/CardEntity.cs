using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Card, Tower
};
public class CardEntity : MonoBehaviour
{
    //스텟
    [Header("Properties")]
    //레벨
    public CardDataSet Card;
    public int level = 1;

    //protected int level = new LevelBasedInt { baseValue = 1 };
    public void upgradeCard(int _level)
    {
        level = _level;
        if(transform.GetChild(0).gameObject.GetComponent<CardRankMgr>() != null)
        {
            transform.GetChild(0).gameObject.GetComponent<CardRankMgr>().InputCardRank();
        }
    }
    //공격력
    protected LevelBasedInt _damage = new LevelBasedInt { baseValue = 1 };
    public virtual int damage
    {
        get
        {
            return _damage.Get(level);
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

    //공격속도(배율 1 => 100%)
    protected LevelBasedFloat _attackSpeed = new LevelBasedFloat { baseValue = 1 };
    public virtual float attackSpeed
    {
        get
        {
            return _attackSpeed.Get(level);
        }
    }
    // Start is called before the first frame update
    // Update is called once per frame
    public virtual void Attack()
    {
        
    }


}
