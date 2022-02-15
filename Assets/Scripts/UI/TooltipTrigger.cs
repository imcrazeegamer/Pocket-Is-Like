using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string content;
    public string header;
    private LTDescr delay;
    public Transform objectTransform;
    void Start()
    {
        objectTransform = transform;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Enable();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Disable();
    }
    public void OnMouseEnter()
    {
        Enable();
    }
    public void OnMouseExit()
    {
        Disable();
    }
    void Enable()
    {
        delay = LeanTween.delayedCall(0.5f, () =>
        {
            TooltipSystem.Toggle(objectTransform, content, header, true);
        });
    }
    void Disable()
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Toggle(objectTransform, content, header, false);
    }
}
