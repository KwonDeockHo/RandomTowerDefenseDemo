using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : Singleton<CardDatabase>
{
    public List<CardDataSet> cards = new List<CardDataSet>();
}
