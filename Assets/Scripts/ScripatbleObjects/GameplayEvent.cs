﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConditionList = GGJ.ConditionList;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "GameplayEvent", order = 1)]
public class GameplayEvent : ScriptableObject
{
    public string eventTitle;
    public string eventDescription;
    public List<ConditionMalabarGroup> conditionList;
    public EventChoice[] choices;

    public bool isReutilisable = false;
    public bool lethal;
    public bool isEpic;
    public Chunk mapPrefab;

    public float timeToResolveEvent; // if < 0.0f, the event is not timed

    public GameplayRessource[] specificTools;
    public GameplayRessource[] specificCharacters;
}
