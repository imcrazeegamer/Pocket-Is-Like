using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BattleHandler : MonoBehaviour
{

    [SerializeField] GameObject[] moveButtons;
    [SerializeField] TextMeshProUGUI battleInfoText;
    List<Monster> Monsters { get => GameData.data.Monsters; set => GameData.data.Monsters = value;}
    int currentMonsterIndex = 0;
    
    void Start()
    {
        for (int i = 0; i < moveButtons.Length; i++)
        {
            ModularAbility ability = CurrentMonster.abilities[i];
            moveButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = ability.name;
            TooltipTrigger Tt = moveButtons[i].GetComponent<TooltipTrigger>();
            Tt.header = ability.name;
            Tt.content = ability.description;
        }
        battleInfoText.text = $"{CurrentMonster.name}'s Turn";
    }

    public void OnMoveSelected(int abilityIndex)
    {
        if (!GameOver)
        {
            StartCoroutine(AblilityHandler(abilityIndex));
        }
        
    }
    private string UseMonsterAbility(Monster monster, Ability selectedAbility,Monster target)
    {
        if (selectedAbility.UseAblilty(monster, target))
        {
            if(monster == CurrentMonster)
            {
                Vector3 pos = CurrentMonster.transform.position + new Vector3(2, 0, 0);
                Instantiate(selectedAbility.projectile, pos, Quaternion.identity);
            }
            else
            {
                Vector3 pos = EnemyMonster.transform.position - new Vector3(2, 0, 0);
                Instantiate(selectedAbility.projectile, pos, Quaternion.Euler(0, 0, 180));
            }
            return $"{monster.name} Used: {selectedAbility.name}";
        }
        return  $"{monster.name} Missed: {selectedAbility.name}";
    }
    private IEnumerator AblilityHandler(int abilityIndex) 
    {
        Ability selectedAbility = CurrentMonster.abilities[abilityIndex];
        //string abilityName = selectedAbility.name;
        Ability enemyAbility = EnemyMonster.GetRandomAbilites();
        battleInfoText.text = "";
        if (Ability.SpeedCalc(CurrentMonster, selectedAbility, EnemyMonster, enemyAbility))
        {
            battleInfoText.text += UseMonsterAbility(CurrentMonster, selectedAbility, EnemyMonster) + "\r\n";
            yield return new WaitForSeconds(1f);
            battleInfoText.text += UseMonsterAbility(EnemyMonster, enemyAbility, CurrentMonster);
        }
        else
        {
            battleInfoText.text += UseMonsterAbility(EnemyMonster, enemyAbility, CurrentMonster) + "\r\n";
            yield return new WaitForSeconds(1f);
            battleInfoText.text += UseMonsterAbility(CurrentMonster, selectedAbility, EnemyMonster);
        }
        if (GameOver)
        {
            battleInfoText.text = "GAME OVER!";
        }
    }

    public bool GameOver { get => !Monsters.TrueForAll(x => x.stats.CurrentHp > 0); }
    public Monster CurrentMonster { get => Monsters[currentMonsterIndex]; }
    public Monster EnemyMonster { get => Monsters[1-currentMonsterIndex]; }
}
