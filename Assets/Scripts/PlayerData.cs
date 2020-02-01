using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public static int timeLeft;
    public static List<GameplayRessource> characters;
    public static List<GameplayRessource> tools;
    public static List<GameplayEvent> eventsDone;

    public static List<GameplayRessource> GetGameplayResourceList(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Characters:
                return characters;
            case ResourceType.Tools:
                return tools;
            default:
                return null;
        }
    }
}
