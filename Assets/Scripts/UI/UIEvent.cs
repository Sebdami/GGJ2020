using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEvent : UIPanel
{
    [Header("Activables")]
    public GameObject lethalIcon;


    [Header("Labels")]
    public TextMeshProUGUI name;
    public TextMeshProUGUI content;

    public UIEventChoicesEntry entryPrefab;

    public RectTransform choicesPlace;


    public override void Show()
    {
        base.Show();

        name.text = WordGenerator.ReplaceSentence( m_GameplayEventManager.currentEvent.eventTitle);
        content.SetText(WordGenerator.ReplaceSentence(m_GameplayEventManager.currentEvent.eventDescription).Replace("\\n", "\n"));

        lethalIcon.SetActive(m_GameplayEventManager.currentEvent.lethal);

        foreach (Transform t in choicesPlace)
            Destroy(t.gameObject);

        for (int i = 0; i < m_GameplayEventManager.currentEvent.choices.Length; ++i)
        {
            UIEventChoicesEntry entry = Instantiate(entryPrefab);
            entry.transform.SetParent(choicesPlace, false);

            entry.FillWithInfo(m_GameplayEventManager.currentEvent.choices[i], this);
        }
    }
}
