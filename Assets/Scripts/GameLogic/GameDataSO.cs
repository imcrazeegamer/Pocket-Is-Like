using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SaveFile", menuName = "GameData")]
public class GameDataSO : ScriptableObject
{
    public List<Player> Players;
}
