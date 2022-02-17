using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
public class BattleHandler : MonoBehaviour
{

    [SerializeField] GameObject[] moveButtons;
    //[SerializeField] TextMeshProUGUI battleInfoText;
    GameData data;
    List<Player> Players { get => data.data.Players; set => data.data.Players = value;}
    int currentMonsterIndex = 0;
    List<(Monster, Monster, Ability)> abilityCalls = new List<(Monster, Monster, Ability)>();
    public List<Monster> CurrentMonsters;
    List<Monster> EnemyMonsters;

    void Awake()
    {
        data = FindObjectOfType<GameData>();
    }
    void Start()
    {
        var spawners = FindObjectsOfType<MonsterSpawner>();
        foreach (var s in spawners)
        {
            if (s.playerid == 0)
            {
                CurrentMonsters = s.Monsters;
            }
            else
            {
                EnemyMonsters = s.Monsters; 
            }
        }
        UpdateAbilityUI();
    }

    private void UpdateAbilityUI()
    {
        Monster SelectedMonster = CurrentMonsters[currentMonsterIndex];
        for (int i = 0; i < moveButtons.Length; i++)
        {
            ModularAbility ability = SelectedMonster.monsterData.abilities[i];
            moveButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = ability.name;
            TooltipTrigger Tt = moveButtons[i].GetComponent<TooltipTrigger>();
            Tt.header = ability.name;
            ability.UpdateDiscriptions();
            Tt.content = ability.description;
        }
        if(currentMonsterIndex > 0)
        {
            CurrentMonsters[currentMonsterIndex - 1].UnitSelectState(false);
        }
        SelectedMonster.UnitSelectState(true);
        //battleInfoText.text = $"{SelectedMonster.name}'s Turn";
        
    }

    public void OnMoveSelected(int abilityIndex)
    {
        if (!GameOver)
        {

            //FIX THIS SHIT DOG
            //Monster target = EnemyMonsters[0];
            List<Monster> targets = (from enemy in EnemyMonsters where enemy.Selected select enemy).ToList();
            if(targets.Count < 1)
            {
                Debug.Log("No Targets selected");
                return;
            }

            Monster user = CurrentMonsters[currentMonsterIndex];
            abilityCalls.Add((user, targets[0], user.monsterData.abilities[abilityIndex]));
            currentMonsterIndex++;
            
        }
        if (currentMonsterIndex >= CurrentMonsters.Count)
        {
            currentMonsterIndex = 0;
            foreach(Monster m in CurrentMonsters)
            {
                m.UnitSelectState(false);
            }
            EnemyTurn();
            StartCoroutine(AblilityHandler());
            

        }
        else
        {
            UpdateAbilityUI();
        }
    }

    private void EnemyTurn()
    {
        //collect all user moves and then.
        foreach (Monster m in EnemyMonsters)
        {
            abilityCalls.Add((m, CurrentMonsters[Random.Range(0, CurrentMonsters.Count)], m.monsterData.abilities[Random.Range(0, m.monsterData.abilities.Count)]));
        }
        
    }
    private string UseMonsterAbility(Monster monster, Monster target, Ability selectedAbility) 
    { 
        Vector3 moveVector, pos;
    
        if (selectedAbility.UseAblilty(monster, target))
        {
            moveVector = target.transform.position - monster.transform.position;
        }
        else
        {
            //missed ability
            moveVector = new Vector3(0, Random.Range(7,11), 0) - monster.transform.position;
        }
        selectedAbility.projectile.GetComponent<Projectile>().MovementDirection = moveVector/200;
        if (CurrentMonsters.Contains(monster))
        {
            pos = monster.transform.position + new Vector3(2, 0, 0);
            //Instantiate(selectedAbility.projectile, pos, Quaternion.identity);
        }
        else
        {
            pos = monster.transform.position - new Vector3(2, 0, 0);
            //Instantiate(selectedAbility.projectile, pos, Quaternion.Euler(0, 0, 180));
        }
        Instantiate(selectedAbility.projectile, pos, Quaternion.identity);
        //return $"{monster.name} Used: {selectedAbility.name}";
        return "";
        
        //return $"{monster.name} Missed: {selectedAbility.name}";
    }
    private IEnumerator AblilityHandler() 
    {
        abilityCalls = Ability.SpeedCalc(abilityCalls);
        foreach((Monster user, Monster target, Ability a) in abilityCalls)
        {
            //battleInfoText.text += UseMonsterAbility(user, target, a) + "\r\n";
            UseMonsterAbility(user, target, a);
            yield return new WaitForSeconds(1f);

            if (Win)
            {
                //battleInfoText.text = "Victory!";
                break;
            }
            else if (Lose)
            {
                //battleInfoText.text = "Defeat!";
                break;
            }
        }
        abilityCalls = new List<(Monster, Monster, Ability)>();
        CurrentMonsters[0].UnitSelectState(true);
    }

    public bool Win { get => EnemyMonsters.TrueForAll(x => x.monsterData.stats.CurrentHp <= 0);}
    public bool Lose { get => CurrentMonsters.TrueForAll(x => x.monsterData.stats.CurrentHp <= 0);}
    public bool GameOver { get => Win || Lose; }
}
