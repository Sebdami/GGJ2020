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
        content.text = m_GameplayEventManager.choiceMade.recapAfterChoice;
 
    }
}
