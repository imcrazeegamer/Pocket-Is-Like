
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool Selected = false;
    public MonsterSO monsterData;
    [SerializeField] GameObject unitSelector;
    void Awake()
    {
        monsterData.stats.Init();
    }
    public ModularAbility GetRandomAbilites()
    {
        return monsterData.abilities[Random.Range(0, monsterData.abilities.Count)];
    }
    public void UnitSelectState(bool select)
    {
        unitSelector.GetComponent<SpriteRenderer>().enabled = select;
        unitSelector.GetComponent<SpriteRenderer>().color = Color.green;
        unitSelector.SetActive(select);
        //Debug.Log($"Upadted Selection Stat {select}");
    }
    public void UnitSetTarget(bool select)
    {
        unitSelector.GetComponent<SpriteRenderer>().enabled = select;
        unitSelector.GetComponent<SpriteRenderer>().color = Color.red;
        unitSelector.SetActive(select);
        //Debug.Log($"Upadted Selection Stat {select}");
    }

    private void OnMouseDown()
    {
        Selected = !Selected;
        UnitSetTarget(Selected);
    }
}
