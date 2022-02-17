using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ability : ScriptableObject
{
    //public string name;
    public GameObject projectile;
    [SerializeField] protected float[] power = { 10f };
    [SerializeField] [Range(0, 1)] protected float speed = 1f;
    [SerializeField] [Range(0, 1)] protected float accuracy = 1f;
    public Gem[] Slots;

    public float[] Power { get => power; }
    public float Speed { get => speed; }
    public float Accuracy { get => accuracy; }

    public virtual bool UseAblilty(Monster user, Monster target)
    {
        if (!AccuracyCheck(user)) 
        {
            return false;
        }
        //target.stats.SetStat(effectedStat, target.stats.GetStat(effectedStat) - power);
        return true;
    }
    protected bool AccuracyCheck(Monster user) 
    {
        float monsterAccuracy = user.monsterData.stats.GetStat(Stats.Accuracy);
        return (accuracy * monsterAccuracy) >= UnityEngine.Random.value;
    }
    protected bool HasEmptySlot()
    {
        return Slots.Any(x => x == null);
    }
    public static bool SpeedCalc(Monster user, Ability selectedAbility, Monster enemy, Ability enemyAbility) 
    {
        float uSpeed = user.monsterData.stats.GetStat(Stats.Speed) + selectedAbility.Speed; 
        float eSpeed = enemy.monsterData.stats.GetStat(Stats.Speed) + enemyAbility.Speed; 
        return SpeedCalc(uSpeed,eSpeed);
    }
    public static List<(Monster, Monster, Ability)> SpeedCalc(List<(Monster, Monster, Ability)> abilityCalls)
    {
        return abilityCalls.OrderBy(x => x.Item3.Speed).ToList();
    }
    public static bool SpeedCalc(float uSpeed, float eSpeed)
    {
        return (uSpeed > eSpeed ||
            (uSpeed == eSpeed && UnityEngine.Random.value >= 0.5));
    }
    

}


