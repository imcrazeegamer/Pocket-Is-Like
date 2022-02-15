using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Gem _gem;
    private GameObject lastParrent;
    public Gem gem { get => _gem; set => _gem = UpdateGem(value); }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = FindObjectOfType<Canvas>();
        lastParrent = transform.parent.gameObject;
    }
    Gem UpdateGem(Gem g)
    {
        if (g != null)
        {
            //GetComponent<Image>().sprite = gem.GemSprite;
        }
        return g;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent.gameObject.GetComponent<ItemSlot>().item = null;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        if (lastParrent != transform.parent.gameObject)
        {
            rectTransform.anchoredPosition = lastParrent.GetComponent<RectTransform>().anchoredPosition;
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
