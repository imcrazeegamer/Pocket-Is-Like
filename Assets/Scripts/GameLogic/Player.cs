using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player", menuName = "Player")]
public class Player : ScriptableObject
{
    public List<MonsterSO> Monsters;
    //public List<Monster> CurrentMonsters;
    //To Implament
    public List<Gem> Inventory;
}
