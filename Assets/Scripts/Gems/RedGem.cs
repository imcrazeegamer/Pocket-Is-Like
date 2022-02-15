
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New RedGem", menuName = "RedGem", order = 1)]
public class RedGem : Gem
{
    [Header("Red Gem Of Stats")]
    [SerializeField] Stats option;

    public new Stats GetValue()
    {
        return option;
    }
    public override string ToString()
    {
        return option.ToString();
    }

}