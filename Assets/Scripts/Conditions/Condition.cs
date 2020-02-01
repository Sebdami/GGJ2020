using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
    public bool Invert;
    public abstract bool Check();
}
