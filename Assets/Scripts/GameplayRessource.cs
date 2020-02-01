using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameplayRessource
{
    public string ressourceName; // if empty, the character is not named
    public bool damaged = false;
    public bool canBeLost = true;

    public GameplayRessource() { }
    public GameplayRessource(string _ressourceName) {
        ressourceName = _ressourceName;
    }

    public GameplayRessource(GameplayRessource _from) {
        ressourceName = _from.ressourceName;
        damaged = _from.damaged;
        canBeLost = _from.canBeLost;
    }

}

[System.Serializable]
public enum ResourceType
{
    Characters,
    Tools
}
