using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRankMgr : MonoBehaviour
{
    public GameObject rankHolder, cardPrefab;
    public GameObject rankPrefab ;
    public int cardRankIndex, childCount;

    // Start is called before the first frame update
    void Start()
    {
        cardPrefab = transform.parent.gameObject;
        rankHolder = transform.gameObject;
        
        if(cardPrefab.GetComponent<CardEntity>() != null)
            InputCardRank();
    }
    public void upgradeCard(int setLevel)
    {

    }
    public void InputCardRank()
    {
        cardRankIndex = cardPrefab.GetComponent<CardEntity>().level;
        childCount = transform.childCount;

        for (int i=0; i < (cardRankIndex - childCount); i++)
        {
            GameObject rankStar = Instantiate(rankPrefab);
            rankStar.transform.SetParent(rankHolder.transform);
            // effect Delete
            for(int j = 0; j < rankStar.transform.childCount; j++)
            {
                Destroy(rankStar.transform.GetChild(j).gameObject, 1.5f);
            }
        }
    }
    
}
