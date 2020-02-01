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
        content.text = choiceRef.choiceDescription;
        gameObject.SetActive(choiceRef.IsChoiceEnabled());
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => SelectAChoice());
    }

    public void SelectAChoice()
    {
        string genericFeedbackCosts = ""; // Peut être utilisé sur le panneau de recap
        genericFeedbackCosts = choiceRef.costs.ResolveCosts();

  

        UIManager.Instance.HidePanel<UIEvent>();

        if(genericFeedbackCosts == string.Empty)
        {
            GameplayEventManager GEM = FindObjectOfType<GameplayEventManager>();
            string nextAction = GEM.choiceMade.ChoiceConsequences();
            if (string.IsNullOrEmpty(nextAction))
            {
                // Close UI
                // UIManager.Instance.
                UIManager.Instance.HideAllPanel();
                // Move

                // Call Trigger event on GEM quand le move est terminé
            }
            else
            {
                // Close UI
                UIManager.Instance.HideAllPanel();

                // Call next event
                GEM.TriggerEvent(nextAction);
            }
        }
        else
        {
            UIManager.Instance.ShowPanel<UIRecap>();
        }

        // TODO: Afficher le panneau de recap
        // On peut set un pavé de texte sur le choix pour le recap
        // choiceRef.recapChoice
    }
}
