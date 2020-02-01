using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameplayEvent : ScriptableObject
{
    public bool lethal;
    public bool isEpic;
    public string eventTitle;
    public string eventDescription;
    public GameObject mapPrefab;

    public float timeToResolveEvent; // if < 0.0f, the event is not timed

    public GameplayRessource[] specificTools;
    public GameplayRessource[] specificCharacters;

    public ConditionList conditionList;
    public EventChoice[] choices;
}
