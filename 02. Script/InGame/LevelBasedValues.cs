using System;

[System.Serializable]
public struct LevelBasedInt
{
    public int baseValue;
    public int bonusPerLevel;
    public int Get(int level) { return baseValue + bonusPerLevel * (level - 1); }
}

[System.Serializable]
public struct LevelBasedFloat
{
    public float baseValue;
    public float bonusPerLevel;
    public float Get(int level) { return baseValue + bonusPerLevel * (level - 1); }
}

public struct LevelBasedDouble
{
    public double baseValue;
    public double bonusPerLevel;
    public double Get(int level) { return baseValue + bonusPerLevel * (level - 1); }
}

