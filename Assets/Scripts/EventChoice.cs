using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventChoice
{ 
    public ConditionList choiceEnabled;
    public ChoiceCost costs;
    public List<string> possibleChainedEvents;
    public float probaChainedEvent = 0.5f;

    public bool IsChoiceEnabled()
    {
        return choiceEnabled.Check();
    }

    /// <summary>
    /// Retourne l'id de l'event suivant, ou vide si on doit se déplacer ensuite
    /// </summary>
    /// <returns></returns>
    public string ChoiceConsequences()
    {
        if (PlayerData.CheckIfGameIsOver())
        {
            // Trigger final event
            return "";
        }
        else
        {
            if (possibleChainedEvents.Count > 0)
            {
                if (Random.Range(0, 1.0f) < probaChainedEvent)
                {
                    // Trigger random chained event
                    return possibleChainedEvents[Random.Range(0, possibleChainedEvents.Count)];
                }
                else
                {
                    // Move to next event
                    return "";
                }
            }
            else
            {
                // Move to next event
                return "";
            }
        }
    }

    [System.Serializable]
    public class ChoiceCost
    {
        public int timeCost;
        public int charactersCost;
        public int toolsCost;

        public bool lethalForRessources;
        public List<string> namedCharacters;
        public List<string> namedTools;

        public void ResolveCosts()
        {
            PlayerData.timeLeft -= timeCost;
            CheckRessources(namedCharacters, true);
            CheckRessources(namedTools, false);

            NotNamedCosts(PlayerData.characters, true);
            NotNamedCosts(PlayerData.tools, false);
        }

        void CheckRessources(List<string> _ids, bool _isCharacter)
        {
            foreach (var id in _ids)
            {
                PlayerData.DamageRessource(id, _isCharacter, lethalForRessources);
            }
        }

        void NotNamedCosts(List<GameplayRessource> _playerData, bool _isCharacter)
        {
            List<GameplayRessource> gameplayRessourcesNotNamed = _playerData.FindAll(x => string.IsNullOrEmpty(x.ressourceName));
            int diff = gameplayRessourcesNotNamed.Count - ((_isCharacter) ? charactersCost : toolsCost);
            for (int i = 0; i < ((_isCharacter) ? charactersCost : toolsCost) && i < gameplayRessourcesNotNamed.Count; i++)
            {
                _playerData.Remove(gameplayRessourcesNotNamed[i]);
            }

            if (diff < 0)
            {
                PlayerData.ShuffleRessources();
                List<GameplayRessource> gameplayRessourcesNamed = _playerData.FindAll(x => !string.IsNullOrEmpty(x.ressourceName));
                for (int i = 0; i < diff && i < gameplayRessourcesNamed.Count; i++)
                {
                    PlayerData.DamageRessource(gameplayRessourcesNamed[i].ressourceName, _isCharacter, lethalForRessources);
                }
            }
        }
    }
}
