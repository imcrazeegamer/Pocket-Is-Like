using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public GameDataSO data;
    //bool dataGenerated = false;
    private void Awake()
    {
        ManageSingleton();
        
    }

    private void ManageSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            data = Resources.Load<GameDataSO>("Saves/DefaultSaveFile");
        }
    }
}
