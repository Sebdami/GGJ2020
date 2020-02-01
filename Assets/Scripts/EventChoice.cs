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

    public class ChoiceCost
    {
        public int timeCost;
        public int charactersCost;
        public int toolsCost;

        public List<string> namedCharacters;
        public List<string> namedTools;

        public void ResolveCosts()
        {
            PlayerData.timeLeft -= timeCost;


        }

        void CheckIfGameIsOver()
        {

        }
    }
}
