using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Monster", menuName = "MonsterData")]
public class MonsterSO : ScriptableObject
{
    public MonsterStats stats;
    public List<ModularAbility> abilities;
    public Sprite sprite;
}
