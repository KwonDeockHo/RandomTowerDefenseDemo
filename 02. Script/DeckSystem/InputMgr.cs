using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InputMgr : MonoBehaviour
{
    //private bool CardSelect = false;
    //마우스의 다운 혹은 터치다운 시에 들어오는 이벤트
    // 카드를 선택했을 경우 마우스 버튼을 이용하여 클릭 이벤트
    int orindex;
    [SerializeField]
    private GameObject CanvasUI;
    [SerializeField]
    private RectTransform panel;
    [SerializeField]
    private Vector2 pointPos = Vector2.zero;

    public Card selectedCard;
    [SerializeField]
    public Card SelectedCard { get => selectedCard; }

    public DropZone MainManuPrefab;
    protected DropZone MainManuInstance;

    public bool setActive;
    [HideInInspector]
    public ControllerMode Mode;

    public enum ControllerMode
    {
        play,
        Build,
        Menu
    }
    private void Start()
    {
        setActive = false;
    }

    public void BeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            // Debug.Log("PointerDown");
            //Debug.Log("OnPointerDown " + eventData.pointerCurrentRaycast.gameObject.name);
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                CardMgr.Instance.SetSelectedCard(eventData.pointerCurrentRaycast.gameObject);
            }

        }
    }

    public void Drag(PointerEventData eventData)
    {
        if (CardMgr.Instance.SelectedCard != null)
        {
            CardMgr.Instance.MoveCard(eventData.position);
        }
    }

    public void EndDrag(PointerEventData eventData)
    {

        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);

        if (eventData.pointerCurrentRaycast.gameObject != null)
        {

            if (result[result.Count-1].gameObject.GetComponent<DropZone>() != null)
            {
              //  Debug.Log("result[result.Count-1] InputCard");
                CardMgr.Instance.InputCard();
                
            }
            else if (result[result.Count - 1].gameObject != null)
            {
               // Debug.Log("result[result.Count-1] UpgradeCard");
                CardMgr.Instance.UpgradeCard();
                eventData.pointerCurrentRaycast.Clear();
            }

        }
        else
        {
            CardMgr.Instance.ReleseCard();
            eventData = null;
        }
    }
}
