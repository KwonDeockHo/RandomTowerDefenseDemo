using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Bulletctrl : MonoBehaviour
{
    private int damage;
    public float speed = 10.0f;

    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    private float distance;
    private float startTime;
    private Vector3 shootDir;
    public GameObject hitEffect;
    
    private GameMgr gameManager;
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
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            coll.gameObject.GetComponent<Enemy>().TakeDamage(damage);

            Destroy(effect, 5f);
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        startTime = Time.time;
        //distance = Vector2.Distance(startPosition, targetPosition);
        gameManager = GameObject.Find("GameManager").GetComponent<GameMgr>();
        damage = 20;

     //   GetComponent<Rigidbody2D>().AddForce(targetPosition.normalized * speed);
    }
    public float GetDamage()
    {
        return this.damage;
    }
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));

        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        float timeInterval = Time.time - startTime;
        //gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);
        transform.position += shootDir * speed * Time.deltaTime;

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
