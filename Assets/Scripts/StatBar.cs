using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{

    [SerializeField] Monster source;

    [SerializeField] Stats max_stat = new Stats();

    [SerializeField] Stats current_stat = new Stats();
    Slider s;

    // Update is called once per frame
    void Update()
    {
        s = GetComponent<Slider>();
        s.maxValue = source.monsterData.stats.GetStat(max_stat);
        s.value = source.monsterData.stats.GetStat(current_stat);
    }
}
