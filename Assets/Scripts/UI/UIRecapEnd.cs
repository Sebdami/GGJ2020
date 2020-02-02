using UnityEngine;
using TMPro;
using System.Linq;

public class UIRecapEnd : UIPanel
{

    [Header("Labels")]
    public TextMeshProUGUI content;

    private GameplayEventManager m_GameplayEventManager;


    public override void Show()
    {
        base.Show();

        m_GameplayEventManager = FindObjectOfType<GameplayEventManager>();

        GameplayEvent[] accomplissement = PlayerData.eventsDone.FindAll(x => x.isEpic = true).ToArray();
        string contentTmp = "$[Heros] had a great life, he ";
        if (accomplissement.Length > 0)
        {
            for (int i = 0; i < accomplissement.Length; i++)
            {
                if(i < accomplissement.Length-1)
                    contentTmp += ("rebuild " + accomplissement[i].eventTitle + " and").ToString() ;
                else
                    contentTmp += ("rebuild " + accomplissement[i].eventTitle + ".").ToString();
            }
        }
  
        content.text = WordGenerator.ReplaceSentence(contentTmp);
    }

    public void CloseRecap()
    {
        GameManager.Instance.NextAction();
    }
}
