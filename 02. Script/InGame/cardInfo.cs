using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using UnityEditor;

public abstract partial class CardInfo : ScriptableObject
{
    [Header("Info")]
    public bool followupDefaultAttack;
    [SerializeField, TextArea(1, 30)] protected string toolTip;
    public Sprite image;
    public bool learnDefault;
    public bool cancelCastIfTargetDied;
    public bool showSelector = true;

    [Header("Learn Requirements per Card Level")]
    public LevelBasedInt requiredLevel;

    [Header("Properties per Card Level")]
    public int maxLevel = 5;
    public LevelBasedFloat attackTime;
    public LevelBasedFloat attackSpeed;
    public LevelBasedFloat castRange;

    public Sprite CardSprite;
    public GameObject TowerPrefab;

    // 객체 타입
    public abstract bool CheckTarget(CardEntity caster);


    // 객체 타입, 스킬 레벨, 거리
    public abstract bool CheckDistance(CardEntity caster, int skillLevel, out Vector3 destination);


    // 객체 타입, 스킬 레벨,
    public abstract void Apply(CardEntity caster, int skillLevel);

    // 레벨, True/false
    public virtual string ToolTip(int level, bool showRequirements = false)
    {
        StringBuilder tip = new StringBuilder(toolTip);
        tip.Replace("{NAME}", name);
        tip.Replace("{LEVEL}", level.ToString());
        tip.Replace("{ATTACKTIME}", attackTime.ToString());
        tip.Replace("{ATTACKSPEED}", attackSpeed.ToString());
        //tip.Replace("{ATTACKTIME}", Utils.PrettyTime(castTime.Get(level)));
        //tip.Replace("{ATTACKSPEED}", Utils.PrettyTime(cooldown.Get(level)));
        tip.Replace("{CASTRANGE}", castRange.Get(level).ToString());

        if (showRequirements)
            tip.Append("\n<b><i>Required Level: " + requiredLevel.Get(1) + "</i></b>\n");

        return tip.ToString();
    }
}