using UnityEngine;
using UnityEngine.Events;

public class Chunk : MonoBehaviour
{
    public Color light1;
    public Color light2;

    public Transform playerTargetTransform;
    public Transform prefabSpawnTransform;
    [System.Serializable]
    public struct LightingSettings
    {
        public Color skyColor;
        public Color equatorColor;
        public Color groundColor;
        public Color skyboxTint;
    }
    public LightingSettings lightingSettings;

    public UnityEvent OnAppear;
    public UnityEvent OnActivate;

    public void Appear()
    {
        OnAppear?.Invoke();
    }

    public void Activate()
    {
        OnActivate?.Invoke();
    }
}
