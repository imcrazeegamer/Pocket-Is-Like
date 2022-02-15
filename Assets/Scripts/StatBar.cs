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
    void Start()
    {
        s = GetComponent<Slider>();
        s.maxValue = source.stats.GetStat(max_stat);
    }

    // Update is called once per frame
    void Update()
    {
        s.value = source.stats.GetStat(current_stat);
    }
}
