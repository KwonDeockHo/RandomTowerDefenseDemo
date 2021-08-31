using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSetMgr : MonoBehaviour
{
    public static CardSetMgr self;
    public GameObject bufferCard;
    public GameObject BufferCard { get => bufferCard; }

    private void Awake()
    {
        if (self)
            Destroy(this);
        else
            self = this;

    }
    public void InputCardMgr(GameObject selectedCard)
    {
        // 기존 카드 업그레이드 (삭제 후 재 생성)
        if (selectedCard != null)
        {
            GameObject cardBuffer = Instantiate(selectedCard);
            cardBuffer.transform.SetParent(transform);
            //cardBuffer.GetComponent<DrawCard>().SetDrawCard(selectedCard);
            //cardBuffer.name = selectedCard.GetName();
            cardBuffer.transform.SetSiblingIndex(transform.childCount);
        }// 새로운 카드 생성


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
