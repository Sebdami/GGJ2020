using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameplayEvent : ScriptableObject
{
    public int timeCost;
    public bool lethal;
    public bool isEpic;
    public string eventTitle;
    public string eventDescription;
    public GameObject mapPrefab;

    public float timeToResolveEvent; // if < 0.0f, the event is not timed

    public GameplayTool[] specificTools;
    public RecruitableCharacter[] specificCharacters;

    public ConditionList conditionList;
}
