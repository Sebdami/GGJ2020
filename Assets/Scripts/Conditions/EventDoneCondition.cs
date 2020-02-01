using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPassedCondition : Condition
{
    public GameplayEvent Event;
    public override bool Check()
    {
        return PlayerData.eventsDone.Contains(Event);
    }
}
