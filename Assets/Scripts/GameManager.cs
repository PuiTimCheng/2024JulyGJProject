using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _singleton;
    public static GameManager Singleton
    {
        get
        {
            if(_singleton == null) _singleton = new GameObject("GameManager").AddComponent<GameManager>();
            return _singleton;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void StartNewGame()
    {
        //load game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    
    public void ShowRanking()
    {
        //show ranking ui here
    }

    public void ShowCredits()
    {
        //show credits ui here
        
    }
    
    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
