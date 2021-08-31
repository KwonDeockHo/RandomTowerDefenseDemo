 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggble : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Transform root;
    [SerializeField]
    public bool DeckStatus;
    // Start is called before the first frame update
    void Start()
    {
        root = transform.root;
    }
    void Awake()
    {
        DeckStatus = true;
    }
    // Update is called once per frame

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
            root.BroadcastMessage("BeginDrag", eventData, SendMessageOptions.DontRequireReceiver);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
                  root.BroadcastMessage("Drag", eventData, SendMessageOptions.DontRequireReceiver);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        root.BroadcastMessage("EndDrag", eventData, SendMessageOptions.DontRequireReceiver);
    }

    public void SetDeckStatus(bool status)
    {
        this.DeckStatus = status;
    }


}
