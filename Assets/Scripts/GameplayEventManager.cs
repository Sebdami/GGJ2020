﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameplayEventManager : MonoBehaviour
{
    public List<GameplayEvent> events = new List<GameplayEvent>();

    private void Awake()
    {
        events = Resources.LoadAll("Events", typeof(GameplayEvent)).Cast<GameplayEvent>().ToList();
    }
    
    public GameplayEvent currentEvent = null;
    public EventChoice choiceMade = null;
    [HideInInspector] public bool nextEventReady = false;

    public void TriggerEvent()
    {
       
        ShuffleEvents();
        GameplayEvent nextEvent;
        if (currentEvent != null)
            nextEvent = events.Find(x => !PlayerData.eventsDone.Contains(x) && x.conditionList.CheckAll() && x != currentEvent);
        else
            nextEvent = events.Find(x => !PlayerData.eventsDone.Contains(x) && x.conditionList.CheckAll());

        if (nextEvent == null)
        {
            Debug.LogError("Can't find an event not done, restart game");
        }
        else
            currentEvent = nextEvent;

        nextEventReady = true;
    }

    public void TriggerEvent(string _eventId)
    {
        if (_eventId == "The End")
        {
            if(!GameManager.Instance.EndMusicIsPlaying)
                AudioManager.Instance.FadeMusic(AudioManager.Instance.endMusic, 2f);
            GameplayEvent endEvent = events.Find(x => x.name == "The End");
            currentEvent = endEvent;
            nextEventReady = true;
            return;
        }


        GameplayEvent nextEvent = events.Find(x => !PlayerData.eventsDone.Contains(x) && x.name == _eventId);
        if (nextEvent == null)
        {
            TriggerEvent();

        }
        else
        {
            currentEvent = nextEvent;
            nextEventReady = true;
        }
    }


    void ShuffleEvents()
    {
        for (int i = events.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i);

            GameplayEvent tmp = events[i];
            events[i] = events[j];
            events[j] = tmp;
        }
    }
}
