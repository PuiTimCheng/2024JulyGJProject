using System;
using System.Collections;
using System.Collections.Generic;
using Battle;
using MoreMountains.Feedbacks;
using TimToolBox.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] MMF_Player loadPlayScenePlayer;
    [SerializeField] MMF_Player loadMenuScenePlayer;

    public GameObject platePrefab;
    public Dictionary<FoodName, FoodData> FoodNameToConfigs;
    
    protected override void InitializeSingleton()
    {
        base.InitializeSingleton();
        //Load Food Configs
        FoodNameToConfigs = new Dictionary<FoodName, FoodData>();
        
        //load using addressable
        platePrefab =  Addressables.LoadAssetAsync<GameObject>("Assets/Prefab/Plate.prefab").WaitForCompletion();
        
        var foodNames = Enum.GetNames(typeof(FoodName));
        foreach (var foodName in foodNames)
        {
            var path = $"Assets/ScriptableObjects/FoodData/FoodData_{foodName}.asset";
            FoodData data = Addressables.LoadAssetAsync<FoodData>(path).WaitForCompletion();
            if(data) FoodNameToConfigs.Add(Enum.Parse<FoodName>(foodName), data);
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
