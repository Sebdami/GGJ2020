using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class Chunk : MonoBehaviour
{
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
