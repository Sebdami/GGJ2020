using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionMalabarGroup
{
    public ConditionMalabar[] ETconditions;

    // condition1 && condition2 && ... && conditionN
    public bool Check()
    {
        foreach (var condition in ETconditions)
        {
            if (!condition.Check())
                return false;
        }

        return true;
    }

   
}
public static class ExtensionMalabarGroup
{
    public static bool CheckAll(this List<ConditionMalabarGroup> conditions)
    {
        if (conditions == null || conditions.Count == 0)
            return true;

        foreach (var condition in conditions)
        {
            if (condition.Check())
                return true;
        }

        return false;
    }
}
