using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEvent : UIPanel
{
    [Header("Labels")]
    public TextMeshProUGUI name;
    public TextMeshProUGUI content;

    public override void Show()
    {
        base.Show();
    }
}
