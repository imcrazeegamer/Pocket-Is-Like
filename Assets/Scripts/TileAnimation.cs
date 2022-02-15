using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileAnimation : MonoBehaviour
{
    [SerializeField] RuleTile tile;
    //[SerializeField] float min;
    //[SerializeField] float max;
    [SerializeField] float delta;
    RuleTile.TilingRule rule;
    Tilemap tmap;
    float baseScale;
    float scale;
    int i = 0;
    BattleHandler bh;
    private void Awake()
    {
        
    }
    void Start()
    {
        tmap = GetComponent<Tilemap>();
        bh = FindObjectOfType<BattleHandler>();
        rule = tile.m_TilingRules[0];
        scale = baseScale = rule.m_PerlinScale;
        StartCoroutine(UpdateTiles());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator UpdateTiles()
    {
        while (!bh.GameOver)
        {
            scale = baseScale + (delta * Mathf.Cos(i/100)) * Time.deltaTime * 0.003f;
            rule.m_PerlinScale = scale;
            tmap.RefreshAllTiles();
            i++;
            yield return new WaitForSeconds(0.5f);
        }

    }
    private void OnApplicationQuit()
    {
        rule.m_PerlinScale = baseScale;
    }
}
