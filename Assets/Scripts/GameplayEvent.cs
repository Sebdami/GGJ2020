using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayEvent
{
    public int timeCost;
    public bool hasBeenPlayed;
    public bool lethal;
    public bool isEpic;
    public string eventTitle;
    public string eventDescription;
    public GameObject mapPrefab;
    public Image sprite;
    public Material material;
}
