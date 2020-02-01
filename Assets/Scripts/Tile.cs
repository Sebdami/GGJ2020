using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public UnityEvent OnAppear;
    public UnityEvent OnActivate;
    Animator animator;

    void Start()
    {
        OnAppear?.Invoke();
    }

    public void Activate()
    {
        OnActivate?.Invoke();
    }
}
