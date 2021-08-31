using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{

    [Header("Unity Stuff")]
    public Transform healthBar;
    private float health;
    // Start is called before the first frame update
    private float OriginalHealth;
    // 체력
    // 공격력
    // 이동 속도
    // 공격 속도
    // 

    public Transform TowerMgr;
    private Enemy _enemy;
    public float _Life = 1000;
    private float Timer;
    public float _enemySpeed;
    public float damage = 1000;
    public float score = 100;
    public bool isDie = false;
    public float slowTime = 0f;
    public float slowValue = 0.3f;
    public Material slow;
    void Awake()
    {
        isDie = false;
        _enemy = GetComponent<Enemy>();
        _Life = 1000;
        health = _Life;
        OriginalHealth = healthBar.transform.localScale.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Timer += Time.deltaTime;

        if(slowTime > 0)
        {
            slowTime -= Time.deltaTime;
            transform.GetComponent<Renderer>().material = slow;

        }
    }
    private void Move()
    {        
        Vector3 dir = TowerMgr.position - transform.position;

        if(slowTime > 0)
            transform.Translate(dir.normalized * (_enemySpeed * slowValue) * Time.deltaTime, Space.World);
        else
            transform.Translate(dir.normalized * _enemySpeed * Time.deltaTime, Space.World);
    }

    public void SlowMove(float value)
    {
        slowTime = value;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Tower")
        {
            TowerDie();
        }

        if (coll.gameObject.tag == "Laser")
        {
            if (Timer >= 0.1f)
            {
                Timer = 0f;
                TakeDamage(coll.gameObject.GetComponent<Bulletctrl>().GetDamage() / 5);

            }
        }

    }
    public void MonsterDie()
    {

    }
    public void TakeDamage(float amount)
    {
        bool isCritical = (Random.Range(0, 100) < 30);
        float isCriticalDamage = (Random.Range(100, 200)) / 100.0f;
        if (isCritical)
            amount *= isCriticalDamage;

        amount = Mathf.FloorToInt(amount);

        if (0 <= _Life)
        {
            _Life -= amount;

        }
        if (_Life <= 0) {
            isDie = true;
        }


        Vector3 tmpScale = healthBar.transform.localScale;
        tmpScale.x = (_Life / health) * OriginalHealth;
        healthBar.transform.localScale = tmpScale;
        //healthBar.rect
        //healthBar = _Life / health;



        DamagePopup.Create(transform.position, (int)amount, isCritical);
        // healthBar.fillAmount = health / startHealth;
        // Debug.Log("체력 다운");

        if (_Life <= 0 && isDie)
        {       
            Debug.Log("_Life" + _Life + ", Damage : " + amount + ", Die : " + isDie);
            Die();
        }
    }
    void Die()
    {
        //GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 1f);
        if (gameObject != null)
        {
            _Life = -1;

            GameObject.Destroy(gameObject);

            ScoreMgr.self.updateScore((int)score);
        }

    }
    void TowerDie()
    {
        //GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 1f);
        Destroy(gameObject);
    }
}
