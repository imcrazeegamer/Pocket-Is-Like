using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New GreenGem", menuName = "GreenGem", order = 1)]
public class GreenGem : Gem
{
    //static List<Element> options = Enum.GetValues(typeof(Element)).Cast<Element>().ToList();
    [Header("Green Gem Of The Elements")]
    [SerializeField] Element option;
    public new Element GetValue()
    {
        return option;
        //if (valueIndex < options.Count)
        //{
        //    return options[valueIndex];
        //}
        //return options[0];
    }
}