using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    private int damage = 1;

    private Vector3 shootDir;
    // 이펙트 생성 이벤트 처리
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    // 총알 충돌체크 이벤트 처리
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
           // Damage(gameObject.transform);
            
            Destroy(gameObject);
        }
    }
    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }
    // 화면 밖으로 벗어나는 오브젝트 처리
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
        
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        float moveSpeed = 10f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }
}
