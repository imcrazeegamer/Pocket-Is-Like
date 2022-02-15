using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Gem : ScriptableObject
{
    public Sprite GemSprite;
    public virtual string GetValue()
    {
        Debug.LogError("Called GEM.GETVALUE, that should not happen, code red");
        return null;
    }
}


