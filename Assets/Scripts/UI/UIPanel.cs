using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIPanel : MonoBehaviour
{
    protected const float FadeTime = 0.15f;
    
    public Ease defaultEaseIn = Ease.Linear;
    public Ease defaultEaseOut = Ease.Linear;

    protected bool _isShown;
    private Tween _tweenFade;
    protected CanvasGroup _canvasGroup;

    protected CanvasGroup CanvasGroup => _canvasGroup;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        InitializeTween();
        _canvasGroup.interactable = false;
    }

    public virtual void ResetPanel()
    {
    }

    public virtual void Show()
    {
        _isShown = true;

        _tweenFade.Kill();
        gameObject.SetActive(true);
             
            TweenCallback onComplete = ShowTween != null ? ShowTween.onComplete : null;

            _tweenFade = ShowTween.OnComplete(() =>
            {
                if (onComplete != null) onComplete();
                _canvasGroup.interactable = true;
            });
             
        _tweenFade.Play();
    }

    public virtual void Hide()
    {
        if (!_isShown)
        {
            return;
        }
 
        _isShown = false;

        _tweenFade.Kill();
        _canvasGroup.interactable = false;
        _tweenFade = HideTween.OnComplete(() => gameObject.SetActive(false));
        _tweenFade.Play();
    }

    protected virtual void InitializeTween()
    {
        _canvasGroup.alpha = 0f;
    }

    protected virtual Tween ShowTween
    {
        get
        {
            float adaptiveTime = (1f - _canvasGroup.alpha) * FadeTime;
            return _canvasGroup.DOFade(1f, adaptiveTime).SetEase(defaultEaseIn);
        }
    }



protected virtual Tween HideTween
    {
        get
        {
            float adaptiveTime = _canvasGroup.alpha * FadeTime;
            return _canvasGroup.DOFade(0f, adaptiveTime).SetEase(defaultEaseOut);
        }
    }
}
