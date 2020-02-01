using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMethods : MonoBehaviour
{
    // Fonction à appeler sur le panneau de recap pour passer à la suite (soit se déplacer vers le prochain point, soit jouer le prochain event)
    public void NextAction()
    {
        GameplayEventManager GEM = FindObjectOfType<GameplayEventManager>();
        string nextAction = GEM.choiceMade.ChoiceConsequences();
        if (string.IsNullOrEmpty(nextAction))
        {
            // Close UI
            // UIManager.Instance.

            // Move

            // Call Trigger event on GEM quand le move est terminé
        }
        else
        {
            // Close UI

            // Call next event
            GEM.TriggerEvent(nextAction);
        }
        
    }


}
