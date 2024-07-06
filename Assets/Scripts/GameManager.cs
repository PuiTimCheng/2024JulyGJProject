using System;
using System.Collections.Generic;
using Battle;
using MoreMountains.Feedbacks;
using TimToolBox.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] MMF_Player loadPlayScenePlayer;
    [SerializeField] MMF_Player loadMenuScenePlayer;

    public GameObject platePrefab;
    public Dictionary<FoodName, FoodData> FoodNameToConfigs;
    public Dictionary<string, int> rankingData = new Dictionary<string, int>();
    public static string RankingDataSaveKey = "RankingDataSaveKey";
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
        
        //load saved ranking data
        rankingData = ES3.Load(RankingDataSaveKey, new Dictionary<string, int>());
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
