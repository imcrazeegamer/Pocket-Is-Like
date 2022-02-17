using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] GameObject abilityPrefab;

    void Start()
    {
        foreach(ModularAbility ability in FindObjectOfType<GameData>().data.Players[0].Monsters[0].abilities)
        {
            GameObject tPrefab = abilityPrefab;
            AbilityUI aui = tPrefab.GetComponent<AbilityUI>();
            aui.ability = ability;
            Instantiate(tPrefab, transform);
        }
    }
}
