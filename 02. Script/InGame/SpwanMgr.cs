using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanMgr : MonoBehaviour
{
    //몬스터가 출현할 위치를 담을 배열
    public Transform[] spawnPoint;

    //몬스터 프리팹을 할당할 변수
    public GameObject[] monsterPrefab;

    public string _SpawnPointTag = "SpawnPoint";

    public Transform enemyPrefab;
    //public Transform spawnPoint;
    public float timeBetweenWaves = 0.5f;

    private float countDown = 2f;
    //몬스터의 최대 발생 개수
    private int maxMonster = 50;

    //몬스터를 발생시킬 주기
    public float createTime = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.Find(_SpawnPointTag).GetComponentsInChildren<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if(countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            
            countDown = timeBetweenWaves;
        }
        countDown -= Time.deltaTime;
    }
    IEnumerator SpawnWave()
    {
        
            SpawnEnmy();
            yield return new WaitForSeconds(createTime);
        
        //waveNumber++;

    }
    void SpawnEnmy()
    {
        if (!GameMgr.Instance.isGameOver)
        {
            //현재 생성된 몬스터 개수 산출
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (monsterCount < maxMonster)
            {
                ////몬스터의 생성 주기 시간만큼 대기
                //yield return new WaitForSeconds(createTime);

                //불규칙적인 위치 산출
                int idx = Random.Range(0, spawnPoint.Length);

                //Debug.Log("Index : " + idx);
                //몬스터의 동적 생성
                int monIdx = Random.Range(0, monsterPrefab.Length);
                Instantiate(monsterPrefab[monIdx], spawnPoint[idx].position, spawnPoint[idx].rotation);
            }
        }

        //Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
