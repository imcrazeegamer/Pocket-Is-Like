using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster Stats",menuName = "Monster Stats")]

public class MonsterStats : ScriptableObject
{

    [SerializeField] float baseHp = 100;
    [SerializeField] [Range(0, 1)] float baseSpeed = 1;
    [SerializeField] [Range(0, 1)] float baseAccuracy = 1;


    Dictionary<Stats, float> statTable = new Dictionary<Stats, float>();
    public float BaseHp { get => statTable[Stats.BaseHp]; }
    public float BaseSpeed { get => baseSpeed; }
    public float BaseAccuracy { get => baseAccuracy; }
    public float CurrentHp { get => statTable[Stats.CurrentHp]; set => statTable[Stats.CurrentHp] = value; }
    public void Init()
    {
        statTable[Stats.BaseHp] = baseHp;
        statTable[Stats.BaseSpeed] = baseSpeed ;
        statTable[Stats.BaseAccuracy] = baseAccuracy ;
        statTable[Stats.CurrentHp] = baseHp;
        statTable[Stats.Speed] = baseSpeed * 0.8f;
        statTable[Stats.Accuracy] = baseAccuracy * 0.8f;
    }
    public float GetStat(Stats stat)
    {
        return statTable[stat];
    }
    public void SetStat(Stats stat,float value)
    {
        statTable[stat] = value;
    }


}
public enum Stats
{
    BaseHp,
    CurrentHp,
    BaseSpeed,
    Speed,
    BaseAccuracy,
    Accuracy,
};

public enum Element
{
    Normal,
}

