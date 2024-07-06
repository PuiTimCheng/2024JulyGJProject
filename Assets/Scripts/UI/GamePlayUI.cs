using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour ,IUIPanel
{
    public Button PauseBtn;
    public Button RestartBtn;

    private void Awake()
    {
        PauseBtn.onClick.AddListener(PlaySceneController.Instance.PauseAndUnPause);
        RestartBtn.onClick.AddListener(GameManager.Instance.LoadPlayScene);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
