using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using UnityEditor;

public abstract partial class SkillTemplate : ScriptableObject {
    [Header("Info")]
    public bool followupDefaultAttack;
    [SerializeField, TextArea(1, 30)] protected string toolTip;
    public Sprite image;
    public bool learnDefault; 
    public bool cancelCastIfTargetDied;
    public bool showSelector = true; 

    [Header("Learn Requirements per Skill Level")]
    public LevelBasedInt requiredLevel;

    [Header("Properties per Skill Level")]
    public int maxLevel = 1;
    public LevelBasedInt manaCosts;
    public LevelBasedFloat castTime;
    public LevelBasedFloat cooldown;
    public LevelBasedFloat castRange;

    public static float ClosestDistance(Collider a, Collider b)
    {
        return Vector3.Distance(a.ClosestPointOnBounds(b.transform.position),
                                b.ClosestPointOnBounds(a.transform.position));
    }

    public static string PrettyTime(float seconds)
    {
        var t = System.TimeSpan.FromSeconds(seconds);
        string res = "";
        if (t.Days > 0) res += t.Days + "d";
        if (t.Hours > 0) res += " " + t.Hours + "h";
        if (t.Minutes > 0) res += " " + t.Minutes + "m";
        if (t.Milliseconds > 0) res += " " + t.Seconds + "." + (t.Milliseconds / 100) + "s";
        else if (t.Seconds > 0) res += " " + t.Seconds + "s";
        return res != "" ? res : "0s";
    }


    // 객체 타입
    public abstract bool CheckTarget(MonsterEntity caster);

    
    // 객체 타입, 스킬 레벨, 거리
    public abstract bool CheckDistance(MonsterEntity caster, int skillLevel, out Vector3 destination);


    // 객체 타입, 스킬 레벨,
    public abstract void Apply(MonsterEntity caster, int skillLevel);

    // 레벨, True/false
    public virtual string ToolTip(int level, bool showRequirements = false) {
        StringBuilder tip = new StringBuilder(toolTip);
        tip.Replace("{NAME}",       name);
        tip.Replace("{LEVEL}",      level.ToString());
        tip.Replace("{CASTTIME}",   PrettyTime(castTime.Get(level)));
        tip.Replace("{COOLDOWN}",   PrettyTime(cooldown.Get(level)));
        tip.Replace("{CASTRANGE}",  castRange.Get(level).ToString());
        tip.Replace("{MANACOSTS}",  manaCosts.Get(level).ToString());

        if (showRequirements)
            tip.Append("\n<b><i>Required Level: " + requiredLevel.Get(1) + "</i></b>\n");

        return tip.ToString();
    }
}