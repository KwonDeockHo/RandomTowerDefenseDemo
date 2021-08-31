using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum CardType
{
    Equipment,//장비
    Consumption,//소모
    Misc//기타
}

[System.Serializable]
public class Card : Tower
{
    // 보여질 카드에 대한 클래스
    private string cardName;
    public int childIndex;

    // 체력 - 이름 - 데미지 - 속성
    public string CardId { get; set; }                  // 타워 레벨
    public int Level { get; set; }                      // 타워 레벨
    public int Hp { get; set; }                         // 타워 현재 체력
    public int MaxHp { get; set; }                      // 타워 최대 체력
    public string Name { get; set; }                    // 타워 이름
    public double Damage { get; set; }                  // 타워 데미지
    public string CardType { get; set; }                // 타워 데미지
    public float BulletCnt { get; set; }                // 타워 발사 수
    public string Descript { get; set; }                // 타워 데미지
    public string BulletType { get; set; }              // 타워 데미지
    public Image CardImg { get; set; }                  // 타워 데미지
    public Sprite CardSprite { get; set; }              // 타워 데미지
    public List<string> Relation { get; set; }          // 타워 데미지
    public Text LevelCnt { get; set; }                  // 타워 레벨 출력
    public GameObject CardToTower { get; set; }         // 타워 레벨 출력


    // Start is called before the first frame update
    public Card(string _CardId, int _Level, int _Hp, int _MaxHp, string _Name, double _Damage, float _DamageCnt, string _Type, string _Descript, Sprite _SpriteImg, string _BulletType)
    {
        CardId = _CardId; // 타워 아이디
        Level = _Level; // 타워 레벨
        Hp = _Hp;    // 타워 현재 체력
        MaxHp = _MaxHp; // 타워 최대 체력
        Name = _Name;  // 타워 이름
        Damage = _Damage;// 타워 데미지
        BulletCnt = _DamageCnt;// 타워 발사 수
        CardType = _Type;  // 타워 데미지
        Descript = _Descript;  // 타워 데미지
        CardSprite = _SpriteImg;
        BulletType = _BulletType;  // 타워 데미지
    }
    public void SetDrawCard(Card other)
    {
        this.CardId = other.CardId; // 타워 아이디
        this.Level = other.Level; // 타워 레벨
        this.Name = other.Name;  // 타워 이름
        this.Damage = other.Damage;// 타워 데미지
        this.BulletCnt = other.BulletCnt;// 타워 발사 수
        this.CardType = other.CardType;  // 타워 데미지
        this.Descript = other.Descript;  // 타워 데미지
        this.BulletType = other.BulletType;
        this.CardSprite = other.CardSprite;
        this.LevelCnt = other.LevelCnt;
    }
    public bool SameCard(Card other)
    {
        if (this.CardId == other.CardId
                && this.Level == other.Level)
            return true;
        else
            return false;
    }
}
