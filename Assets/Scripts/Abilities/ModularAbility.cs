using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ablilty", menuName = "Modular Ablilty")]
public class ModularAbility : Ability
{
    [SerializeField] AblityType abilityType;
    AblityType _lastChanged;
    Dictionary<AblityType, string> descriptions;
    public string description { get => descriptions[abilityType]; }
    int validationCounter = 0;
    public override bool UseAblilty(Monster user, Monster target)
    {
        if (HasEmptySlot())
        {
            Debug.LogError($"Empty Gem Slot on ablitty: {this.name}");
            return false;
        }
        if (!AccuracyCheck(user))
        {
            return false;
        }
        switch (abilityType)
        {
            case AblityType.Blast:
                return Blast(user, target);
            case AblityType.Heal:
                return Heal(user, target);
            case AblityType.StatMod:
                return StatMod(user, target);
        }

        return false;
    }
    private bool Blast(Monster user, Monster target)
    {
        Stats stat = ((RedGem)Slots[0]).GetValue();
        target.monsterData.stats.SetStat(stat, target.monsterData.stats.GetStat(stat) - power[0]);
        return true;
    }
    private bool Heal(Monster user, Monster target)
    {
        Stats stat = ((RedGem)Slots[0]).GetValue();
        user.monsterData.stats.SetStat(stat, user.monsterData.stats.GetStat(stat) + power[0]);
        return true;
    }
    private bool StatMod(Monster user, Monster target)
    {
        Stats userStat = ((RedGem)Slots[0]).GetValue();
        Stats targetStat = ((RedGem)Slots[1]).GetValue();
        user.monsterData.stats.SetStat(userStat, user.monsterData.stats.GetStat(userStat) + power[0]);
        target.monsterData.stats.SetStat(targetStat, target.monsterData.stats.GetStat(targetStat) - power[1]);
        return true;
    }
    public void UpdateDiscriptions()
    {
        //string value = $"{name} deals {_arr_string(power,0)} to {_gem_string<RedGem>(0)} Stat";
        descriptions = new Dictionary<AblityType, string>()
            {
                {AblityType.Blast, $"{name} deals {_arr_string(power,0)} to Enemys {_gem_string<RedGem>(0)},\r\nAccuracy: {Accuracy}, Speed: {Speed}"},
                {AblityType.Heal, $"{name} heals {_arr_string(power,0)} of your {_gem_string<RedGem>(0)},\r\nAccuracy: {Accuracy}, Speed: {Speed}"},
                {AblityType.StatMod, $"{name} adds {_arr_string(power,0)} to your {_gem_string<RedGem>(0)},\r\nDeals {_arr_string(power,1)} to Enemys {_gem_string<RedGem>(1)},\r\nAccuracy: {Accuracy}, Speed: {Speed}"},
            };
    }
    string _gem_string<T>(int slotIndex)
    {
        return Slots.ElementAtOrDefault(slotIndex) == null ? "__" : ((T)Convert.ChangeType(Slots[slotIndex], typeof(T))).ToString();
    }
    string _arr_string(float[] arr,int index)
    {
        return (power.ElementAtOrDefault(index) == 0 ? "null" : arr[index].ToString());
    }

    void _onValidate()
    {
        
        if (_lastChanged != abilityType && validationCounter > 0)
        {
            int modAmount = 0;
            switch (abilityType)
            {
                case AblityType.Blast:
                    modAmount = 1;
                    break;
                case AblityType.Heal:
                    modAmount = 1;
                    break;
                case AblityType.StatMod:
                    modAmount = 2;
                    break;
            }
            Slots = new Gem[modAmount];
            power = new float[modAmount];
            _lastChanged = abilityType;
            //OnValidate();
        }
        UpdateDiscriptions();
        //description = (descriptions.ContainsKey(abilityType) ? descriptions[abilityType] : "No Type Found");
        validationCounter++;
    }
}
public enum AblityType
{
    Blast,
    Heal,
    StatMod,
}
