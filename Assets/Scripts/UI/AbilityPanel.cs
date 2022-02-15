using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] GameObject abilityPrefab;

    void Start()
    {
        foreach(ModularAbility ability in GameData.data.Monsters[0].abilities)
        {
            GameObject tPrefab = abilityPrefab;
            AbilityUI aui = tPrefab.GetComponent<AbilityUI>();
            aui.ability = ability;
            Instantiate(tPrefab, transform);
        }
    }
}
