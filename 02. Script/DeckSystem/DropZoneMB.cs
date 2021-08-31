using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneMB : MonoBehaviour
{ 
    public static DropZoneMB self;
    
    // 데이터 보관 용
    public DropZonePiece dropZonePiecePrefab;
    public float gapWidthDegree = 1f;
    public Action<string> callback;

    // UI는 자동 업데이트가 안되므로, 해당 데이터로 새로 셋업
    [SerializeField]
    private DropZonePiece[] Pieces;
    protected DropZone Parent;
    private Transform root;

    [SerializeField]
    private bool setActive = false;

    [HideInInspector]
    public string Path;
    // Start is called before the first frame update
    void Awake()
    {
        //CardUpdate();

        if (self)
            Destroy(this);
        else
            self = this;

        gameObject.SetActive(setActive);

    }
    public void CardUpdate()
    {
        var stepLength = 360f / transform.childCount;
        var iconDist = Vector3.Distance(dropZonePiecePrefab.GetPositionCake().transform.position, dropZonePiecePrefab.CakePiece.transform.position);
        //Pieces = dropZoneMenu.Elements.ToArray();
        Pieces = new DropZonePiece[transform.childCount];

        //Debug.Log("Pieces call : " + Pieces.Length);
        for (int i = 0; i < transform.childCount; i++)
        {
            Pieces[i] = transform.GetChild(i).gameObject.GetComponent<DropZonePiece>();

            // Piece 범위(등분을 위한 수식)
            if(transform.childCount <= 1)
                Pieces[i].CakePiece.fillAmount = 1f / transform.childCount;
            else
                Pieces[i].CakePiece.fillAmount = 1f / transform.childCount - gapWidthDegree / 360f;

            Pieces[i].CakePiece.transform.localPosition = Vector3.zero;

            // Piece Rotation(회전 값)
            Pieces[i].CakePiece.transform.localRotation = Quaternion.Euler(0, 0, -stepLength / 2f + gapWidthDegree / 2f + i * stepLength);
           // Debug.Log("DrawCard " + i + " : " + Pieces[i].GetComponent<DrawCard>().gameObject.name);

            Pieces[i].gameObject.GetComponentInChildren<Draggble>().gameObject.transform.localPosition = Pieces[i].CakePiece.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
            Pieces[i].gameObject.GetComponentInChildren<Draggble>().SetDeckStatus(false);

            Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.5f);
        }
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    //Pieces[i] = dropZoneMenu.Elements[i];
        //    Pieces[i] = Instantiate(dropZoneMenu.Elements[i], transform);
        //    //Debug.Log("dropZonePiecePrefab : " + dropZonePiecePrefab);
        //    //set root element
        //    Pieces[i].transform.localPosition = Vector3.zero;
        //    Pieces[i].transform.localRotation = Quaternion.identity;

        //    //set cake piece

        //    // Piece 범위(등분을 위한 수식)
        //    Pieces[i].CakePiece.fillAmount = 1f / dropZoneMenu.Elements.Count - gapWidthDegree / 360f;
        //    Pieces[i].CakePiece.transform.localPosition = Vector3.zero;

        //    // Piece Rotation(회전 값)
        //    Pieces[i].CakePiece.transform.localRotation = Quaternion.Euler(0, 0, -stepLength / 2f + gapWidthDegree / 2f + i * stepLength);
        //    Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.5f);

        //    //Pieces[i].GetCakePieceObj().transform.localPosition = Pieces[i].CakePiece.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
        //    //Pieces[i].CopyGameObject(dropZoneMenu.Elements[i].GetCakePieceObj());
        //    //Pieces[i].transform.SetParent(transform);
        //}
    }
    private void Update()
    {
        if (transform.childCount > 0)
        {
            var stepLength = 360f / transform.childCount;
            var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - transform.position, Vector3.forward) + stepLength / 2f);
            var activeElement = (int)(mouseAngle / stepLength);

            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == activeElement)
                    Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.75f);
                else
                    Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
    }
    public void InputCardMgr(GameObject selectedCard)
    {
        // 기존 카드 업그레이드 (삭제 후 재 생성)
        if (selectedCard != null)
        {
            DropZonePiece newPiece = Instantiate(dropZonePiecePrefab, transform);
            // Debug.Log(newPiece);
            newPiece.transform.localPosition = Vector3.zero;
            newPiece.transform.localRotation = Quaternion.identity;
            newPiece.CakePiece.color = new Color(1f, 1f, 1f, 0.5f);
            
            GameObject setDrawCard = Instantiate(selectedCard, newPiece.transform);
            setDrawCard.transform.SetParent(newPiece.transform);
            setDrawCard.GetComponent<Draggble>().SetDeckStatus(false);
            setDrawCard.transform.position = newPiece.positionCard.transform.position;

            CardUpdate();
        }// 새로운 카드 생성
    }

    public void SetDropActive(bool validState)
    {
        if (transform.childCount > 0)
        {
            var stepLength = 360f / transform.childCount;
            var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - transform.position, Vector3.forward) + stepLength / 2f);
            var activeElement = (int)(mouseAngle / stepLength);
            //DropZoneMgr.self.Elements.Count
            DropZonePiece SetPiece = transform.GetChild(activeElement).gameObject.GetComponent<DropZonePiece>();
            var path = Path + "/" + SetPiece.name;

            //if (SetPiece.nextDropZone != null)
            //{
            //    //var newSubRing = Instantiate(gameObject, transform.parent).GetComponent<DropZoneMB>();
            //    ////newSubRing.Parent = this;
            //    //for (var j = 0; j < newSubRing.transform.childCount; j++)
            //    //    Destroy(newSubRing.transform.GetChild(j).gameObject);
            //    //newSubRing.dropZoneMenu = SetPiece.nextDropZone;
            //    //newSubRing.Path = path;
            //    //newSubRing.callback = callback;
            //}
            //else
            //{
            //    callback?.Invoke(path);
            //}

            callback?.Invoke(path);
        }

        Debug.Log("DropZone 클릭 : " + setActive);
        setActive = !setActive;
        gameObject.SetActive(setActive);
    }

    private float NormalizeAngle(float a) => (a + 360f) % 360f;
}
