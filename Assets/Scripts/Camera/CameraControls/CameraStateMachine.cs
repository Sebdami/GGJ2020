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

    bool init = false;

    public void Init(Transform followTarget)
    {
        // RUN STATE
        cm = new CameraController(Camera.main, transform, DefaultSettings);
        ctc = new CameraThemeController();
        cm.SetFollowTarget(followTarget);
        cm.SetDesiredSettings(DefaultSettings.targetSettings, DefaultSettings.duration);

        init = true;
    }

    public void ZoomIn(Action calback = null)
    {
        DoCameraShake(0.5f, delegate () { calback?.Invoke(); cm.SetDesiredSettings(OtherSettings.targetSettings, OtherSettings.duration); });

    }

    public void ZoomOut(Action calback = null)
    {
        DoCameraShake(0.5f, delegate () { calback?.Invoke(); cm.SetDesiredSettings(DefaultSettings.targetSettings, DefaultSettings.duration); });
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(init)
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
