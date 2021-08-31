using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMgr : Singleton<TimeMgr>
{
    //public static TimeMgr self;

    [SerializeField]
    private float TimeText;
    public Text time;
    [SerializeField]
    private bool timeGoing;

    private TimeSpan timePlaying;
    // Start is called before the first frame update
    private void Awake()
    {
        //if (self)
        //    Destroy(this);
        //else
        //    self = this;

        TimeText = 0;
        //time = transform.GetChild(0).gameObject.GetComponent<Text>();
        time.text = "0:00.00";
        timeGoing = false;
    }
    //void Start()
    //{
    //    TimeText = 0;
    //    //time = transform.GetChild(0).gameObject.GetComponent<Text>();
    //    time.text = "0:00.00";
    //    timeGoing = false;
    //}
    public void BeginTimer()
    {
        timeGoing = true;
        TimeText = 0f;
        Debug.Log("Time Start : " + timeGoing);
        StartCoroutine(UpdateTimer());
    }
    public void EndTimer()
    {
        timeGoing = false;
    }
    // Update is called once per frame
    private IEnumerator UpdateTimer()
    {
        while (timeGoing) {
            TimeText += Time.deltaTime;

            timePlaying = TimeSpan.FromSeconds(TimeText);
            string timePlayingStr = "" + timePlaying.ToString("mm':'ss'.'ff");
            time.text = timePlayingStr;

            yield return null;
        }
    }
}
