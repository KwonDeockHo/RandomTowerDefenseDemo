using System.Text;
using UnityEngine;
using UnityEditor;

public struct Skill
{
    public SkillTemplate template;

    public string name;

    public bool learned;
    public int level;
    public double castTimeEnd; 
    public double cooldownEnd;

    
    public Skill(SkillTemplate _template)
    {
        template = _template;
        name = _template.name;
        learned = _template.learnDefault;
        level = 1;

        castTimeEnd = cooldownEnd = Time.time;
    }

    public float castTime { get { return template.castTime.Get(level); } }
    public float cooldown { get { return template.cooldown.Get(level); } }
    public float castRange { get { return template.castRange.Get(level); } }
    public int manaCosts { get { return template.manaCosts.Get(level); } }
    public bool followupDefaultAttack { get { return template.followupDefaultAttack; } }
    public Sprite image { get { return template.image; } }
    public bool learnDefault { get { return template.learnDefault; } }
    public bool cancelCastIfTargetDied { get { return template.cancelCastIfTargetDied; } }
    public bool showSelector { get { return template.showSelector; } }
    public int maxLevel { get { return template.maxLevel; } }
    public int requiredLevel { get { return template.requiredLevel.Get(1); } }
    public int upgradeRequiredLevel { get { return template.requiredLevel.Get(level + 1); } }
    public bool CheckTarget(MonsterEntity caster) { return template.CheckTarget(caster); }
    public bool CheckDistance(MonsterEntity caster, out Vector3 destination) { return template.CheckDistance(caster, level, out destination); }
    public void Apply(MonsterEntity caster) { template.Apply(caster, level); }
    public string ToolTip()
    {
        StringBuilder tip = new StringBuilder(template.ToolTip(level, !learned));

        if (learned && level < maxLevel)
            tip.Append("\n<b><i>Upgrade Required Level: " + upgradeRequiredLevel + "</i></b>\n");

        return tip.ToString();
    }

    public float CastTimeRemaining()
    {
        return Time.time >= castTimeEnd ? 0 : (float)(castTimeEnd - Time.time);
    }

    public bool IsCasting()
    {
        return CastTimeRemaining() > 0;
    }

    public float CooldownRemaining()
    {
        return Time.time >= cooldownEnd ? 0 : (float)(cooldownEnd - Time.time);
    }

    public bool IsReady()
    {
        return CooldownRemaining() == 0;
    }
}