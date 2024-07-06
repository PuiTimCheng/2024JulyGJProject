using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TimToolBox.Extensions;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] MMF_Player loadPlayScenePlayer;
    [SerializeField] MMF_Player loadMenuScenePlayer;

    protected override void InitializeSingleton()
    {
        base.InitializeSingleton();
        if (loadPlayScenePlayer == null)
            loadPlayScenePlayer = transform.Find("LoadPlaySceneMMFPlayer").GetComponent<MMF_Player>();
        
        if (loadMenuScenePlayer == null)
            loadMenuScenePlayer = transform.Find("LoadMenuSceneMMFPlayer").GetComponent<MMF_Player>();
    }

    public void LoadPlayScene()
    {
        loadPlayScenePlayer.PlayFeedbacks();
    }

    public void LoadMenuScene()
    {
        loadMenuScenePlayer.PlayFeedbacks();
    }
    
    public void ShowRanking()
    {
        
    }

    public void ShowCredits()
    {
        
    }
}
