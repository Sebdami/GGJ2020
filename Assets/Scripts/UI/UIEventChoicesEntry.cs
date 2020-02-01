using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIEventChoicesEntry : MonoBehaviour
{
    [Header("Labels")]
    public TextMeshProUGUI content;

    private EventChoice choiceRef;

    public void FillWithInfo(EventChoice e,  UIEvent owner)
    {
        choiceRef = e;
        gameObject.SetActive(choiceRef.IsChoiceEnabled());
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => SelectAChoice());
    }

    public void SelectAChoice()
    {
        string genericFeedbackCosts = ""; // Peut être utilisé sur le panneau de recap
        genericFeedbackCosts = choiceRef.costs.ResolveCosts();

        content.text = genericFeedbackCosts;

        // TODO: Afficher le panneau de recap
        // On peut set un pavé de texte sur le choix pour le recap
        // choiceRef.recapChoice
    }
}
