using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMgr : Singleton<CardMgr>
{
    [SerializeField]
    public GameObject cardPrefab, dummyCardPrefab;
    private GameObject cardHolder, parentHolder;
    //cardHolder    : 카드 영역
    //cardPrefab    : 
    //parentHolder  : 카드 드랍하는 가장 상위 객체

    [SerializeField]
    private int k, childCount, Vertical, selectedCardIndex;
    private int CardSpeed = 30;
    private int MaxCardCnt = 10;

    [SerializeField]
    private GameObject selectedCard, hoverCard;
    //private DrawCard previousCard, nextCard;

    // selectedCard : 선택한 카드, 
    private GameObject dummyCardObj;
    // 가릴 더미 카드 프리팹 
   
    private static int callCnt = 0;
    public GameObject SelectedCard { get => selectedCard; }
    public GameObject HoverCard { get => hoverCard; }
    public void MakeCard()
    {
        // 더미 카드 위치(좌측 상단)

        // 카드 덱 부분 위치
        bool keepGoing = true;

        // 카드 이동 
        if (keepGoing == true)
        {
            var step = CardSpeed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(transform.position, cardPrefab.transform.position, step);
        }
    }
    // Awake는 스크립트가 실행할때 발생하는 함수
    void Awake()
    {
        // "함수명", time, repeatRate
        InvokeRepeating("SpawnCard", 5.0f, 5);

     
        cardHolder = transform.gameObject;
        parentHolder = transform.parent.gameObject;
        //Debug.Log("Screen Size = " + Screen.width);

    }

    public void SetSelectedCard(GameObject card)
    {        

        // InputMgr에서 선택한 카드를 해당 프리팹으로 설정
        selectedCard = card;
        // 기존 순번에 삽입

        //// 더미 카드로 대체 
        GetDummyCard().SetActive(true);

        //// SetSiblingIndex() 부모 오브젝트에서 N번째 순서를 지정하는 함수        
        GetDummyCard().transform.SetSiblingIndex(selectedCardIndex);
        
        //temp
        selectedCard.transform.SetParent(parentHolder.transform);

        // 변수에 카드 전체(cardHolder)에 하위 카드들의 갯수 변수 저장
        childCount = cardHolder.transform.childCount;

    }


    // 카드 이동?
    public void MoveCard(Vector2 postion)
    {
        if (selectedCard != null)
        {
            selectedCard.transform.position = postion;
            childCount = cardHolder.transform.childCount;

            //CheckWithNextCard();                            //check for next card
            //CheckWithPreviousCard();
            CheckHoverCard();
        }
    }
    bool ContainPos(RectTransform rt, Vector2 Pos)
    {
        //Pos위치가 rt에 위치하여 있다면 True를 반환
        return RectTransformUtility.RectangleContainsScreenPoint(rt, Pos);
    }

    public void CheckHoverCard()
    {
        for (int i = 0; i < cardHolder.transform.childCount; i++)
        {
            if (ContainPos(cardHolder.transform.GetChild(i).GetComponent<RectTransform>(), selectedCard.transform.position))
            {
                hoverCard = cardHolder.transform.GetChild(i).gameObject;
                hoverCard.transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
            }
            else
            {
                cardHolder.transform.GetChild(i).localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }

    // 선택한 카드 해제
    public void ReleseCard()
    {
        // 선택한 카드가 없을 때
        if(selectedCard != null)
        {
            Destroy(GetDummyCard());
            //// 더미카드 해제
            GetDummyCard().SetActive(false);

            // 선택한 카드를 다시 CardHolder로 이동
            // 1번 과제 : 특정 공간 삽입 시 Delete

            // 2번 과제 : 특정 공간 삽입 하지 않을 경우 다시 덱으로 이동
                selectedCard.transform.SetParent(cardHolder.transform);
            // 같은 자리에 복사(순서 변경)
            //selectedCard.transform.SetSiblingIndex(selectedCard.childIndex);

            // DummyCard 프리팹을 CardHorlder로 이동
            //GetDummyCard().transform.SetParent(parentHolder.transform);
            hoverCard = null;
            selectedCard = null;
            selectedCardIndex = 0;
        }
        if (hoverCard != null)
        {
            hoverCard.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            hoverCard = null;
        }
    }

    public void InputCard()
    {
        // 선택한 카드가 없을 때
        if (selectedCard != null)
        {
            // TowerMgr 카드 input
            if (TowerMgr.self.CardSet.Count < 10)
            {
                // 타워 Input 함수 실행

                GetDummyCard().SetActive(false);

                // 더미카드 해제
                Destroy(GetDummyCard());

                // 실제 게임 오브젝트에 삽입
                TowerMgr.self.TowerInput(selectedCard);
                //Debug.Log("Input card : " + TowerMgr.self.TowerSet.Count);

                DropZoneMB.self.InputCardMgr(selectedCard);

                // 카드 버퍼 인설트
                CardSetMgr.self.InputCardMgr(selectedCard);


                // 선택한 카드가 Tower 위에 안착 했을 경우 삭제
                Destroy(selectedCard);

                // DummyCard 프리팹을 CardHorlder로 이동
                GetDummyCard().transform.SetParent(parentHolder.transform);

                selectedCard = null;                
            }
            else
            {
                ReleseCard();
                // Debug.Log("over card : " + TowerMgr.self.TowerSet.Count);
            }
        }
    }
    public void UpgradeCard()
    {
        // 선택한 카드가 없을 때
        if (selectedCard != null && hoverCard != null)
        {
            ///   GetDummyCard().SetActive(false);

            // 더미카드 해제
            // Destroy(GetDummyCard());
            if (selectedCard.GetComponent<CardEntity>().Card == hoverCard.GetComponent<CardEntity>().Card)
            {
                Debug.Log("같은 카드");
                // 선택한 카드가 Tower 위에 안착 했을 경우 삭제
                //hoverCard.GetComponent<CardEntity>().level = selectedCard.GetComponent<CardEntity>().level + 1;
                //hoverCard
                // CreatedCard(k, hoverCard.transform.GetSiblingIndex(), hoverCard);
                hoverCard.GetComponent<CardEntity>().upgradeCard(selectedCard.GetComponent<CardEntity>().level + 1);
                // 업그레이드 된 상태에서의 

                // 별 생성??


                // 레벨 세팅, 타워 발사 수, 
                //CheckHoverCard();

                Destroy(selectedCard.transform.gameObject);
                //Destroy(hoverCard.transform.gameObject);

                // DummyCard 프리팹을 CardHorlder로 이동
                GetDummyCard().transform.SetParent(parentHolder.transform);
                selectedCard = null;
                
                ReleseCard();
            }
            else
            {
                ReleseCard();
            }
        }
        else
        {
            ReleseCard();
            // Debug.Log("over card : " + TowerMgr.self.TowerSet.Count);
        }
    }
    // 카드 생성
    void CreatedCard(int positionIndex)
    {
        // 기존 카드 업그레이드 (삭제 후 재 생성)
        GameObject card = Instantiate(cardPrefab);
        card.transform.SetParent(cardHolder.transform);

        int index = Random.Range(0, CardDatabase.Instance.cards.Count);
        //card.GetComponent<DrawCard>().SetDrawCard(GameMgr.Instance.drawCardData[index]);
        card.GetComponent<Image>().sprite = CardDatabase.Instance.cards[index].CardSprite;
        card.name = CardDatabase.Instance.cards[index].name;
        card.GetComponent<CardEntity>().Card = CardDatabase.Instance.cards[index];

        card.transform.SetSiblingIndex(positionIndex);
    }

    // 카드 자동 생성 함수
    void SpawnCard()
    {
        childCount = cardHolder.transform.childCount;

        if (childCount < MaxCardCnt) {
            
            //k = 7;
            CreatedCard(childCount);
        }
    }

    // 대체 카드 
    public GameObject GetDummyCard()
    {
        if (dummyCardObj != null)
        {
            if (dummyCardObj.transform.parent != cardHolder.transform)
            {
                dummyCardObj.transform.SetParent(cardHolder.transform);
            }
            return dummyCardObj;

        }
        else
        {
            dummyCardObj = Instantiate(dummyCardPrefab);
            dummyCardObj.name = "DummyCard";
            dummyCardObj.transform.SetParent(cardHolder.transform);
        }
        return dummyCardObj;
    }

    public void CardStatus()
    {
        for (int i = 0; i < cardHolder.transform.childCount; i++)
        {
            if (hoverCard)
            {
                hoverCard = cardHolder.transform.GetChild(i).gameObject;
                hoverCard.transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
            }
            else
            {
                cardHolder.transform.GetChild(i).localScale = Vector3.one;
            }
        }
    }
}

