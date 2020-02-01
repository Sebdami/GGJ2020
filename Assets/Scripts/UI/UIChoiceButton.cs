using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChoiceButton : MonoBehaviour
{
    // Set le choix ici quand le panneau s'ouvre
    public EventChoice choiceRef;

    private void OnEnable()
    {
        gameObject.SetActive(choiceRef.IsChoiceEnabled());
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => SelectAChoice());
    }

    // Fonction à appeler au clic
    public void SelectAChoice()
    {
        string genericFeedbackCosts = ""; // Peut être utilisé sur le panneau de recap
        genericFeedbackCosts = choiceRef.costs.ResolveCosts();

        // TODO: Afficher le panneau de recap
        // On peut set un pavé de texte sur le choix pour le recap
        // choiceRef.recapChoice
    }
}
