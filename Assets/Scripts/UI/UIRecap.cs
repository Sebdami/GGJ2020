using UnityEngine;
using TMPro;

public class UIRecap : UIPanel
{

    [Header("Labels")]
    public TextMeshProUGUI content;

    private bool test = false;



    public override void Show()
    {
        base.Show();
        test = false;
        content.text = WordGenerator.ReplaceSentence(m_GameplayEventManager.choiceMade.recapAfterChoice);
    }

    public void CloseRecap()
    {
        if (!test)
        {
            test = true;
            GameManager.Instance.WaitForAlterPrefab();
        }
    }
}
