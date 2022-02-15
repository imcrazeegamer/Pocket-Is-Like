using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public GameObject item;
    
    [SerializeField] GameObject itemPrefab;
    Type itemType = null;
    public Type ItemType { get => itemType; set => itemType = UpdateType(value); }
    Image slotImage;
    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }
    Type UpdateType(Type type) 
    {
        if (type == typeof(RedGem))
        {
            slotImage.color = new Color(255, 0, 0);
        }
        return type;
    }

    public Gem GetGem()
    {
        DragDrop d;
        if (item != null && item.TryGetComponent(out d))
        {
            return d.gem;
        }
        return null;
    }
    public bool SetGem(Gem g)
    {
        DragDrop d;
        if (item == null)
        {
            item = Instantiate(itemPrefab, transform);
        }
        if (item.TryGetComponent(out d))
        {
            d.gem = g;
            return true;
        }
        return false;
    }
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            item = eventData.pointerDrag;
            Gem gem = item.GetComponent<DragDrop>().gem;
            Type gemType = gem.GetType();
            if (itemType == gemType || itemType == null)
            {
                item.transform.SetParent(transform);
                item.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            }
            else
            {
                //item.GetComponent<RectTransform>().anchoredPosition = item.transform.parent.GetComponent<RectTransform>().anchoredPosition;
                Debug.Log("wrong Type of gem");
            }
            

        }
    }
}
