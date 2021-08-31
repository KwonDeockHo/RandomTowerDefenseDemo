using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//리시버
public class Tower : CardEntity
{
    public Transform target;
    public Enemy targetEnemy;
    public Transform partToRotate;
    public float turnSpeed = 5f;       // 타워 회전 속도
    public string enemyTag = "Enemy";   // 적 태그 
    public float range = 10.7f;          // 공격 범위

    void Start()
    {
        //UpdateTarget을 0f초 이후에 0.5f초마다 실행
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            //타겟 없고 레이저 쓸때
            return;
        }

        LOCKONTARGET();

        //Debug.Log("Target : " + target);

        // 타겟 지정

    }

    //  모든 무기에 공통적으로 들어가는 변수 선언
    public virtual void ATTACK() {

    }

    public virtual void LOCKONTARGET()
    {
        if (target == null)
            target.position = Vector3.zero;

        Vector3 dir = target.position - transform.position;
        var angle = Quaternion.FromToRotation(Vector3.up, dir).eulerAngles.z;
        Quaternion lookRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        // Lerp  0 ~ 100의 가중치 값이 회전하면서 가중치 값이 변하기 때문에
        // 초기에는 빠르게 회전 하나 이후에는 천천히 회전 함.

        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(new Vector3(0, 0, rotation.z));
    }
    public virtual void UPDATETARGET()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        //Enemy 게임오브젝트를 모음
        foreach (GameObject enemy in enemies)
        {
            //해당 오브젝트의 위치와 적 오브젝트의 거리
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                //두 거리를 해당변수에 저장
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        //적이 있고 range거리보다 적 거리가 작으면
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;

            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }

}
