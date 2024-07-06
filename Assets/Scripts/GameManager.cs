using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TimToolBox.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] MMF_Player loadPlayScenePlayer;
    [SerializeField] MMF_Player loadMenuScenePlayer;

    public GameObject foodPrefab;
    public Dictionary<FoodName, FoodConfig> FoodNameToConfigs;
    
    protected override void InitializeSingleton()
    {
        base.InitializeSingleton();
        //Load Food Configs
        FoodNameToConfigs = new Dictionary<FoodName, FoodConfig>();
        
        //load using addressable
        foodPrefab =  Addressables.LoadAssetAsync<GameObject>("Assets/Prefab/Food.prefab").WaitForCompletion();
        
        var foodNames = Enum.GetNames(typeof(FoodName));
        foreach (var foodName in foodNames)
        {
            var path = $"Assets/ScriptableObjects/FoodConfig/FoodConfig_{foodName}.asset";
            var op = Addressables.LoadAssetAsync<FoodConfig>(path);
            FoodConfig config = op.WaitForCompletion();
            if(config) FoodNameToConfigs.Add(Enum.Parse<FoodName>(foodName), config);
        }
    }

    public void LoadPlayScene()
    {
        loadPlayScenePlayer.Initialization();
        loadPlayScenePlayer.PlayFeedbacks();
    }

    public void LoadMenuScene()
    {
        loadMenuScenePlayer.Initialization();
        loadMenuScenePlayer.PlayFeedbacks();
    }
    
    public void ShowRanking()
    {
        
    }

    public void ShowCredits()
    {
        
    }
}
