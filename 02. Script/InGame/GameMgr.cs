using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;


public class GameMgr:Singleton<GameMgr>
{
    //게임 종료 여부 변수
    public bool isGameOver = false;
    public List<Card> drawCardData;
    // Use this for initialization

    void Start()
    {
        // 타이머 시작s
        TimeMgr.Instance.BeginTimer();
    }
    private void Awake()
    {
        CardDataSet(); 
    }
    void CardDataSet()
    {
        //(string TowerId, int Level, int Hp, int MaxHp, string Name, double Damage, double DamageCnt, string Type, string Descript, Sprite SpriteImg, string BulletType)
        drawCardData.Add(new Card("NOR110001", 1, 0, 0, "Normal",           5.00, 1.00f, "Normal",           "Cannon_Bullet",                Resources.Load<Sprite>("04. Sprites/Card/Normal"),           "Bullet"));
        drawCardData.Add(new Card("DOU110001", 1, 0, 0, "Double",           1.50, 2.00f, "Double",           "Cannon_Bullet",                Resources.Load<Sprite>("04. Sprites/Card/Double"),           "Bullet"));
        drawCardData.Add(new Card("EXP110001", 1, 0, 0, "Explosion",        3.50, 1.00f, "Explosion",        "Fire Bullet",                  Resources.Load<Sprite>("04. Sprites/Card/Explosion"),        "Explosion_bullet"));
        drawCardData.Add(new Card("DEX110001", 1, 0, 0, "D_Explosion",      2.50, 2.00f, "D_Explosion",      "Explosion_bullet",             Resources.Load<Sprite>("04. Sprites/Card/D_Explosion"),      "Explosion_bullet"));
        drawCardData.Add(new Card("MEX110001", 1, 0, 0, "Max_Explosion",    2.00, 3.00f, "Max_Explosion",    "Max_Explosion Bullet",         Resources.Load<Sprite>("04. Sprites/Card/Max_Explosion"),    "Cannon_Bullet"));
        drawCardData.Add(new Card("DMI110001", 1, 0, 0, "D_Missile",        2.50, 2.00f, "D_Missile",        "Cannon_Bullet",                Resources.Load<Sprite>("04. Sprites/Card/D_Missile"),        "Cannon_Bullet"));
        drawCardData.Add(new Card("TMI110001", 1, 0, 0, "T_Missile",        2.00, 3.00f, "T_Missile",        "Cannon_Bullet",                Resources.Load<Sprite>("04. Sprites/Card/T_Missile"),        "Cannon_Bullet"));
        drawCardData.Add(new Card("LSR110001", 1, 0, 0, "Laser",            3.50, 1.00f, "Laser",            "Laser_Bullet",                 Resources.Load<Sprite>("04. Sprites/Card/Laser"),            "Laser_Bullet"));

        // Debug.Log("GameMgr 호출, DaraCardData : " + drawCardData.Count);
    }

   
}
