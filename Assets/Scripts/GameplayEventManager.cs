using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

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

    public void Start()
    {
        TriggerEvent("L'appel de l'aventure");
        UIManager.Instance.ShowPanel<UIEvent>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TriggerEvent();
            CameraStateMachine.Instance.defaultTransform.DOMove(new Vector3(10, 0, 0), 2f).OnComplete(() => { UIManager.Instance.ShowPanel<UIEvent>(); }).Play();
        }
    }
    public void TriggerEvent()
    {
        ShuffleEvents();
        GameplayEvent nextEvent = events.Find(x => !PlayerData.eventsDone.Contains(x) && x.conditionList.Check());
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
        GameplayEvent nextEvent = events.Find(x => !PlayerData.eventsDone.Contains(x) && x.conditionList.Check() && x.name == _eventId);
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
