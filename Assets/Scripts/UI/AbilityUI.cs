using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AbilityUI : MonoBehaviour
{
    public ModularAbility ability;
    [SerializeField] GameObject slotsContainerGameObject;
    [SerializeField] GameObject slotPrefab;
    List<ItemSlot> slots = new List<ItemSlot>();
    TextMeshProUGUI textBox;
    
    void Awake()
    {
        textBox = GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
        for (int i = 0; i < ability.Slots.Length; i++)
        {
            GameObject gObject = Instantiate(slotPrefab, slotsContainerGameObject.transform);
            ItemSlot iSlot = gObject.GetComponent<ItemSlot>();
            iSlot.ItemType = typeof(RedGem);// CHANGE THIS LINE 
            if (ability.Slots[i] != null)
            {
                iSlot.SetGem(ability.Slots[i]);
            }
            slots.Add(iSlot);
        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ability.Slots.Length; i++)
        {
            ability.Slots[i] = slots[i].GetGem();
        }
        ability.UpdateDiscriptions();
        textBox.text = ability.description;
    }
}
