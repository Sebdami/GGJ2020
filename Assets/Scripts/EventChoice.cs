using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventChoice
{ 
    public ConditionList choiceEnabled;
    public ChoiceCost costs;

    public bool IsChoiceEnabled()
    {
        return choiceEnabled.Check();
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

        void CheckIfGameIsOver()
        {
            
        }
    }
}
