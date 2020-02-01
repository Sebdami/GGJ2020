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

    private GameplayEventManager m_GameplayEventManager;


    public override void Show()
    {
        base.Show();
        m_GameplayEventManager = FindObjectOfType<GameplayEventManager>();

        name.text = m_GameplayEventManager.currentEvent.eventTitle;
        content.text = m_GameplayEventManager.currentEvent.eventDescription;
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
