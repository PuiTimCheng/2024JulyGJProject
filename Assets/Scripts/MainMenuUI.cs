using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button startBtn;
    public Button rankingBtn;
    public Button creditsBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(GameManager.Singleton.StartNewGame);
        rankingBtn.onClick.AddListener(GameManager.Singleton.ShowRanking);
        creditsBtn.onClick.AddListener(GameManager.Singleton.ShowCredits);
    }
}
