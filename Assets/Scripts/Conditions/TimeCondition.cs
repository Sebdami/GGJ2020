using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimeCondition : InvertibleCondition
{
    public int time;
    public Comparison comparison;

    public override bool Check()
    {
        bool result = ComparisonOperation.CheckComparison(comparison, time, PlayerData.timeLeft);
        return Invert ? !result : result;
    }
}
