
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterStats stats;
    public List<ModularAbility> abilities;
    void Awake()
    {
        stats.Init();
    }
    public ModularAbility GetRandomAbilites()
    {
        return abilities[Random.Range(0, abilities.Count)];
    }
}
