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
    public string recapChoice;

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

        // Variables feedback
        [Header("Variables feedback")]
        public bool showTimeCostOnRecap = true;
        public bool showCharacterCostOnRecap = true;
        public bool showToolsCostOnRecap = true;
        public bool showSpecificCharactersRecap = true;
        public bool showSpecificToolsRecap = true;

        public string overrideRecapCharacter = "";
        public string overrideRecapTool = "";
        public string overrideRecapCharacterDeath = "";
        public string overrideRecapToolBroken = "";


        public string ResolveCosts()
        {
            string feedback = "";
            PlayerData.timeLeft -= timeCost;
            if (showTimeCostOnRecap)
                feedback += "Cette action vous a pris " + timeCost + " unités de temps. ";

            CheckRessources(namedCharacters, true, feedback);
            CheckRessources(namedTools, false, feedback);

            NotNamedCosts(PlayerData.characters, true, feedback);
            NotNamedCosts(PlayerData.tools, false, feedback);

            return feedback;
        }

        void CheckRessources(List<string> _ids, bool _isCharacter, string _feedback)
        {
            foreach (var id in _ids)
            {
                if (_isCharacter)
                {
                    if (showSpecificCharactersRecap)
                        _feedback = PlayerData.DamageRessource(id, _isCharacter, lethalForRessources, _feedback, overrideRecapCharacter, overrideRecapCharacterDeath);
                    else
                        PlayerData.DamageRessource(id, _isCharacter, lethalForRessources, null, "", "");
                }
                else
                {
                    if (showSpecificToolsRecap)
                        _feedback = PlayerData.DamageRessource(id, _isCharacter, lethalForRessources, _feedback, overrideRecapTool, overrideRecapToolBroken);
                    else
                        PlayerData.DamageRessource(id, _isCharacter, lethalForRessources, null, "", "");
                }
            }
        }

        void NotNamedCosts(List<GameplayRessource> _playerData, bool _isCharacter, string _feedback)
        {
            List<GameplayRessource> gameplayRessourcesNotNamed = _playerData.FindAll(x => string.IsNullOrEmpty(x.ressourceName));
            int diff = gameplayRessourcesNotNamed.Count - ((_isCharacter) ? charactersCost : toolsCost);
            for (int i = 0; i < ((_isCharacter) ? charactersCost : toolsCost) && i < gameplayRessourcesNotNamed.Count; i++)
            {
                _playerData.Remove(gameplayRessourcesNotNamed[i]);
            }

            if (_isCharacter && showCharacterCostOnRecap)
                _feedback += "Vous avez perdu " + ((diff > 0) ? charactersCost : charactersCost + diff) + " unités. ";

            if (!_isCharacter && showToolsCostOnRecap)
                _feedback += "Vous avez perdu " + ((diff > 0) ? toolsCost : toolsCost + diff) + " outils. ";

            if (diff < 0)
            {
                PlayerData.ShuffleRessources();
                List<GameplayRessource> gameplayRessourcesNamed = _playerData.FindAll(x => !string.IsNullOrEmpty(x.ressourceName));
                for (int i = 0; i < diff && i < gameplayRessourcesNamed.Count; i++)
                {
                    if (_isCharacter)
                    {
                        if (showSpecificCharactersRecap)
                            _feedback = PlayerData.DamageRessource(gameplayRessourcesNamed[i].ressourceName, _isCharacter, lethalForRessources, _feedback, overrideRecapCharacter, overrideRecapCharacterDeath);
                        else
                            PlayerData.DamageRessource(gameplayRessourcesNamed[i].ressourceName, _isCharacter, lethalForRessources, null, "", "");
                    }
                    else
                    {
                        if (showSpecificToolsRecap)
                            _feedback = PlayerData.DamageRessource(gameplayRessourcesNamed[i].ressourceName, _isCharacter, lethalForRessources, _feedback, overrideRecapTool, overrideRecapToolBroken);
                        else
                            PlayerData.DamageRessource(gameplayRessourcesNamed[i].ressourceName, _isCharacter, lethalForRessources, null, "", "");
                    }
                }
            }
        }
    }
}
