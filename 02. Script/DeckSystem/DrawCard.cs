using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class DrawCard : MonoBehaviour
{
    [SerializeField]
    public Card card { get; set; }
    public int childIndex { get; set; }
    public GameObject rankHolder;
    public GameObject rankPrefab;
    //public GameObject rankHolder { get; set; }
    //public GameObject rankPrefab { get; set; }

    public void SetDrawCard(Card other)
    {
        card = other;
        transform.GetComponent<Image>().sprite = other.CardSprite;
        Debug.Log("card Set : " + other.CardId);

        UpgradeCardRank();
    }

    public void UpgradeCardRank()
    {
        int tmpCount = card.Level - rankHolder.transform.childCount;
        Debug.Log("UpdateCardRank : " + tmpCount);
        if (tmpCount > 0)
        {
            for (int i = 0; i < tmpCount; i++)
            {
                GameObject rankStar = Instantiate(rankPrefab);
                rankStar.transform.SetParent(rankHolder.transform);
            }
        }
        else
        {
            for (int i = 0; i < Mathf.Abs(tmpCount); i++)
            {
                Destroy(rankHolder.transform.GetChild(i));
            }
        }        
    }

}
