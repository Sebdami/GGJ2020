using CameraControls;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;

public class CameraStateMachine : Singleton<CameraStateMachine>
{
    public CameraController cm;
    public CameraTransformData DefaultSettings;

    public CameraTransformData OtherSettings;
    public CameraThemeController ctc;

    public Transform defaultTransform;

    public void Start()
    {
        // RUN STATE
        cm = new CameraController(Camera.main, transform, DefaultSettings);
        cm.SetFollowTarget(defaultTransform);
        cm.SetDesiredSettings(DefaultSettings.targetSettings, DefaultSettings.duration);

        ctc = new CameraThemeController();

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DoCameraShake(2, delegate () { cm.SetDesiredSettings(OtherSettings.targetSettings, OtherSettings.duration); });
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
         cm.Update(Time.deltaTime, true);
    }

    public void ReturnToDefault()
    {
        cm.SetDesiredSettings(DefaultSettings.targetSettings, 1);
    }

    public void ReturnToPreview()
    {
        cm.SetDesiredSettings(cm.previousSettings, 1);
    }

    // Default
    public void DoCameraShake(float duration = 0.5f, Action onComplete = null, float strength= 0.1f)
    {
        Camera.main.DOShakePosition(duration, strength).OnComplete(()=>{
            onComplete?.Invoke();

        }).Play();
    }

 
}
