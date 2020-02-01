using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition
{
    public Condition()
    {

    }

    public bool test;
    public virtual bool Check()
    {
        return true;
    }
}
