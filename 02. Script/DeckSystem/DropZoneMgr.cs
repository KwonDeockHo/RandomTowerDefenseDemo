using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropZoneMgr : Singleton<DropZoneMgr>
{
    [SerializeField]
    public List<DropZonePiece> Elements;
    public GameObject DropHolder;

    private void Awake()
    {
        Elements = new List<DropZonePiece>();
    }
    
    public void InputMenuElement(GameObject card)
    {

        // Elements.Add(selectedCard);
        
        // DropZoneMB.self.CardUpdate();


    }

    public int getElementsCnt()
    {
        return this.Elements.Count;
    }
}
