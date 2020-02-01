using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventPassedCondition : InvertibleCondition
{
    public GameplayEvent Event;
    public override bool Check()
    {
        bool result = PlayerData.eventsDone.Contains(Event);
        return Invert ? !result : result;
    }
}
