using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCondition : InvertibleCondition
{
    public int time;
    public Comparison comparison;

    public override bool Check()
    {
        bool result = false;
        switch (comparison)
        {
            case Comparison.Equal:
                result = time == PlayerData.timeLeft;
                break;
            case Comparison.GreaterThan:
                result = time > PlayerData.timeLeft;
                break;
            case Comparison.LowerThan:
                result = time < PlayerData.timeLeft;
                break;
            default:
                break;
        }
        return Invert ? !result : result;
    }
}
