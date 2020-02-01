using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public static int timeLeft;
    public static List<GameplayRessource> characters;
    public static List<GameplayRessource> tools;
    public static List<GameplayEvent> eventsDone;
    public static List<GameplayRessource> charactersLost;

    public static void DamageRessource(string _ressourceName, bool _isCharacter, bool _destroy)
    {
        GameplayRessource ressource;
        if (_isCharacter)
            ressource = characters.Find(x => x.ressourceName == _ressourceName);
        else
            ressource = tools.Find(x => x.ressourceName == _ressourceName);

        if (ressource != null)
        {
            // Trouver un moyen de communiquer qui est mort ce tour ci 
            if (ressource.damaged || _destroy)
            {
                if (_isCharacter)
                {
                    charactersLost.Add(ressource);
                    characters.Remove(ressource);
                }
                else
                    tools.Remove(ressource);
            }

            ressource.damaged = true;
        }
    }

    public static void ShuffleRessources()
    {
        Shuffle(characters);
        Shuffle(tools);
    }

    static void Shuffle(List<GameplayRessource> _array)
    {
        for (int i = _array.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i);

            GameplayRessource tmp = _array[i];
            _array[i] = _array[j];
            _array[j] = tmp;
        }
    }

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

    public static bool CheckIfGameIsOver()
    {
        if (charactersLost.Find(x => !x.canBeLost) != null)
            return true;

        if (timeLeft < 0)
            return true;

        return false;
    }
}
