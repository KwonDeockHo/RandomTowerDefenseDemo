using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMgr : MonoBehaviour
{
    public static TowerMgr self;
    //CommandManager commandMgr = null;

    [Header("Unity Stuff")]
    public Image healthBar;             // 체력바
    private float _TowerHealth;         // 초기 체력
    public float _TowerLife = 100;      // 현재 체력

    public List<GameObject> CardSet;
    public GameObject Center;


    //[Header("Tower Status")]
    // 타워 정보 업데이트

    void Start()
    {
        if (self)
            Destroy(this);
        else
            self = this;

        //UpdateTarget을 0f초 이후에 0.5f초마다 실행
        //InvokeRepeating("UpdateTarget", 0f, 0.005f);
        _TowerHealth = _TowerLife;
    }
    
    // 적과 충돌 시 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            TowerDamage(collision.gameObject.GetComponent<Enemy>().damage);
        }
    }
    void TowerDamage(float damage)
    {
        _TowerLife -= damage;
        healthBar.fillAmount = _TowerLife / _TowerHealth;
        //Debug.Log("Tower 체력 다운" + "count : " + cnt);
        if (_TowerLife <= 0)
            _TowerLife = 0;
    }
    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    //기즈모를 원형으로 그림. 해당 오브젝트의 포지션에서 range 크기 만큼
    //    Gizmos.DrawWireSphere(transform.position, range);

    //    //타겟중인 오브젝트를 기즈모로 표시 (실행은 되지만 오류가 뜸)
    //    //Gizmos.DrawLine(transform.position, target.transform.position);
    //}
    public void TowerInput(GameObject card)
    {
        //Debug.Log("카드 이름 : " + card.GetName());
        CardSet.Add(card);
        
        string _CenterTag = "Center";

        // Center 위치를 받아옴
        //center = GameObject.Find(_CenterTag).GetComponentsInChildren<Transform>();
        // Debug.Log("CENTER" + center[0].transform);

         Center = GameObject.Find(_CenterTag);
        // Tower 생성, 해당 오브젝트 처리 시 표현
        GameObject game = Instantiate(card.GetComponent<CardEntity>().Card.TowerPrefab, Center.transform);

        
        // TowerCannon = Instantiate(Resources.Load("03. Prefabs/Card/" + card.GetName()) as GameObject, Center.transform);
        //TowerCannon.name = card.Name;
        // Center의 종속으로 하위 오브젝트로 생성
        //TowerCannon.transform.parent = Center.transform;
        
        // 타워 세팅
        //SetTower();
    }
    public void SetTower()
    {
        //float[] tmpBullet = new float[CardSet.Count];

        //for (int i=0; i < CardSet.Count; i++)
        //{
        //    Damage += CardSet[i].Damage;
        //    tmpBullet[i] = CardSet[i].BulletCnt;
        //}
        //BulletCnt = Mathf.Max(tmpBullet);
    }
}
