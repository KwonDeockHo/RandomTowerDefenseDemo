using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMgr : MonoBehaviour
{
    public static ScoreMgr self;

    [SerializeField]
    private float _Score;
    public Text _ScoreText;
    private bool ScoreGoing;
    //private TimeSpan ScorePlaying;
    // Start is called before the first frame update
    void Start()
    {
        if (self)
            Destroy(this);
        else
            self = this;
        _Score = 0f;
        _ScoreText.text = "0 점";

    }

    // Update is called once per frame
    public void updateScore(int score)
    {
        ScoreGoing = true;
        _Score += score;
            string scorePlayingStr = _Score + "점";
            _ScoreText.text = scorePlayingStr;
        

    }
    public void EndScore()
    {
        ScoreGoing = false;
    }
    // Update is called once per frame
}
