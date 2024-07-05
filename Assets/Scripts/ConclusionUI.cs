using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConclusionUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Button restartBtn;
    public Button rankingBtn;
    public Button menuBtn;

    public void Start()
    {
        restartBtn.onClick.AddListener(GameManager.Singleton.StartNewGame);
        rankingBtn.onClick.AddListener(GameManager.Singleton.ShowRanking);
        menuBtn.onClick.AddListener(GameManager.Singleton.BackToMenu);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        scoreText.text = GameSceneController.Singleton.GameData.Score.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
