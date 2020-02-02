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
        content.text = WordGenerator.ReplaceSentence(choiceRef.choiceDescription);
        gameObject.SetActive(choiceRef.IsChoiceEnabled());
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => SelectAChoice());
    }

    public void SelectAChoice()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        choiceRef.costs.ResolveCosts();
        choiceRef.rewards.Gain();
        FindObjectOfType<GameplayEventManager>().choiceMade = choiceRef;
        GameManager.Instance.AchoicewasMade(choiceRef.recapAfterChoice);

            

        // TODO: Afficher le panneau de recap
        // On peut set un pavé de texte sur le choix pour le recap
        // choiceRef.recapChoice
    }
}
