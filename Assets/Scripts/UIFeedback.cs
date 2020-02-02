using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFeedback : MonoBehaviour
{
    public float fadeSpeed;

    private void OnEnable()
    {
        if (activeFade != null)
        {
            StopCoroutine(activeFade);
        }

        activeFade = StartCoroutine(Fade());
    }

    Coroutine activeFade = null;
    IEnumerator Fade()
    {
        float lerpParam = 0.0f;
        while (lerpParam < 1.0f)
        {
            lerpParam += Time.deltaTime * fadeSpeed;
            GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, 1 - lerpParam);
            yield return new WaitForEndOfFrame();
        }

        activeFade = null;
        gameObject.SetActive(false);
    }
}
