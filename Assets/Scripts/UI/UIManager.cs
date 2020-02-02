using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] Image slider;
    [SerializeField] Text people;
    [SerializeField] Text tools;
    [SerializeField] Image sablier;

    [SerializeField] GameObject feedbackTools;
    [SerializeField] GameObject feedbackCharacters;

    [SerializeField] Color positiveColor;
    [SerializeField] Color negativeColor;

    public enum EDirection
    {
        LeftToRight = 0,
        RightToLeft = 1,
        TopToBottom = 2,
        BottomToTop = 3,
    }

    [SerializeField]
    private UIPanel[] uipanels = null;

    //private UIPanel[] runtimePanels;
    private List<UIPanel> runtimePanels = new List<UIPanel>();

    public Transform defaultLayout;

    public T ShowPanel<T>() where T : UIPanel
    {
        T panel = GetPanel<T>();
        
        panel.Show();
        panel.transform.SetParent(defaultLayout, false);
        panel.transform.SetAsLastSibling();
        
        return panel;
    }

    public T HidePanel<T>() where T : UIPanel
    {
        T panel = GetPanel<T>();
        
        panel.Hide();

        return panel;
    }

    protected override void SingletonAwake()
    {
        //runtimePanels = new UIPanel[uipanels.Length];

        for (int i = 0; i < uipanels.Length; i++)
        {
            UIPanel runtimePanel = Instantiate(uipanels[i], defaultLayout).GetComponent<UIPanel>();
            runtimePanel.gameObject.SetActive(false);

            runtimePanels.Add(runtimePanel);
        }
    }

    public void ResetPanels()
    {
        foreach (UIPanel panel in runtimePanels)
        {
            panel.ResetPanel();
        }
    }

    public void HideAllPanel()
    {
        foreach (UIPanel panel in runtimePanels)
        {
            panel.Hide();
        }
    }

    public T GetPanel<T>() where T : UIPanel
    {
        foreach (UIPanel panel in runtimePanels)
        {
            if (panel is T uiPanel)
            {
                return uiPanel;
            }
        }

        return null;
    }

    public void RefreshData()
    {
        slider.fillAmount = ((float)PlayerData.timeLeft / PlayerData.totalTime);
        people.text = PlayerData.characters.Count.ToString();
        tools.text = PlayerData.tools.Count.ToString();
    }

    public void BumpSablier()
    {
        sablier.rectTransform.DOPunchScale(Vector3.one, 0.5f);
    }


    public void PopFeedbackUI(int nextCharactersFeedback, int nextToolsFeedback)
    {
        PopFeedback(nextCharactersFeedback, feedbackCharacters);
        PopFeedback(nextToolsFeedback, feedbackTools);
    }

    void PopFeedback(int _value, GameObject _feedback)
    {
        if (_value != 0)
        {
            _feedback.GetComponentInChildren<Text>().text = "";
            if (_value > 0)
            {
                _feedback.GetComponentInChildren<Text>().color = positiveColor;
                _feedback.GetComponentInChildren<Text>().text = "+";
            }
            else
            {
                _feedback.GetComponentInChildren<Text>().color = negativeColor;
            }

            _feedback.SetActive(true);
        }
        else
            _feedback.SetActive(false);
    }
}
