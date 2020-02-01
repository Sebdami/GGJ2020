using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpecialResourceCondition : InvertibleCondition
{
    public ResourceType resourceType;
    public string specialResourceName;

    public override bool Check()
    {
        List<GameplayRessource> resourceList = PlayerData.GetGameplayResourceList(resourceType);
        bool result = resourceList.FindIndex(x => x.ressourceName == specialResourceName) != -1;
        return Invert ? !result : result;
    }
}
