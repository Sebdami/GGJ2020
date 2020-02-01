using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameplayResourceCondition : InvertibleCondition
{
    public ResourceType resourceType;
    public Comparison comparison;
    public int resourceAmount;

    public override bool Check()
    {
        List<GameplayRessource> resourceList = PlayerData.GetGameplayResourceList(resourceType);
        bool result = ComparisonOperation.CheckComparison(comparison, resourceAmount, resourceList.Count);
        return Invert ? !result : result;
    }
}
