using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;
    public Tooltip tooltip;
    public void Awake()
    {
        current = this;
    }

    public static void Toggle(Transform objectTransform, string content, string header, bool onOff)
    {
        current.tooltip.objectTransform = objectTransform;
        current.tooltip.SetText(content, header);
        current.tooltip.gameObject.SetActive(onOff);
    }
    
}
