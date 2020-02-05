using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearByScript : MonoBehaviour
{
    [SerializeField]
    Transform toScale;

    [SerializeField]
    AnimationCurve appearAnimation;

    [SerializeField]
    float animationDuration;

    public void Start()
    {
        toScale.localScale = Vector3.zero;
    }

    public void Appear()
    {
        StartCoroutine(AppearCoroutine());
    }
    IEnumerator AppearCoroutine()
    {
        float timer = 0f;
        while(timer < animationDuration)
        {
            timer += Time.deltaTime;
            toScale.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, appearAnimation.Evaluate(timer / animationDuration));
            yield return null;
        }
        toScale.localScale = Vector3.one;
    }
}
