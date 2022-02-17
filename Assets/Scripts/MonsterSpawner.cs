using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public int playerid;
    public List<Monster> Monsters;

    private void Awake()
    {
        //var data = FindObjectOfType<GameData>();
        var data = Resources.Load<GameDataSO>("Saves/DefaultSaveFile");
        Monsters = GetComponentsInChildren<Monster>().ToList();
        List<MonsterSO> monsterScripts = data.Players[playerid].Monsters;
        for (int i = 0; i < monsterScripts.Count; i++)
        {
            Monsters[i].monsterData = monsterScripts[i];
        }
    }
}
