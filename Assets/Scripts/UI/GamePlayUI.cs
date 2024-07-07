using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour ,IUIPanel
{
    public Button PauseBtn;
    public Button RestartBtn;
    public Button SettingBtn;

    private void Awake()
    {
        PauseBtn.onClick.AddListener(PlaySceneController.Instance.PauseAndUnPause);
        RestartBtn.onClick.AddListener(GameManager.Instance.LoadPlayScene);
        SettingBtn.onClick.AddListener(GameManager.Instance.LoadMenuScene);
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
