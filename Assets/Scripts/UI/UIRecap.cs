using UnityEngine;
using TMPro;

public class UIRecap : UIPanel
{

    [Header("Labels")]
    public TextMeshProUGUI content;

    private GameplayEventManager m_GameplayEventManager;


    public override void Show()
    {
        base.Show();

        m_GameplayEventManager = FindObjectOfType<GameplayEventManager>();


        content.text = WordGenerator.ReplaceSentence(m_GameplayEventManager.choiceMade.recapAfterChoice);
    }

    public void CloseRecap()
    {
        GameManager.Instance.WaitForAlterPrefab();
    }
}
