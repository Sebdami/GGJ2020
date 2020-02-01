using System.Collections;
using UnityEngine;
public class CoroutineManager : Singleton<CoroutineManager>
{
    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    static public Coroutine StartStaticCoroutine(IEnumerator coroutine)
    {
        return Instance.StartCoroutine(coroutine);
    }

    public IEnumerator TimedDestroy(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }

    public IEnumerator TimedRelease(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
