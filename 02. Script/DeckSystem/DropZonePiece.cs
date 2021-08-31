using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropZonePiece : MonoBehaviour
{
    //public Image Icon;
    public Image CakePiece;
    public GameObject positionCard;
    [SerializeField]
    public GameObject cardData;
    //public DropZoneMenu nextDropZone;
    //private DropZonePiece DropPieces;
    //public DropZonePiece dropPieces { get => DropPieces; }

    public DropZonePiece()
    {
        
        
    }
    private void Awake()
    {
        
    }

    public GameObject GetPositionCake()
    {
        return this.positionCard;
    }
}
                               