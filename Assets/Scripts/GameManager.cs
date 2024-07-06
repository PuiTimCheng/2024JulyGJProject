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
        FoodNameToConfigs = new Dictionary<FoodName, FoodData>();
    
        platePrefab = Addressables.LoadAssetAsync<GameObject>("Assets/Prefab/Plate.prefab").WaitForCompletion();
    
        var foodNames = Enum.GetNames(typeof(FoodName));
        foreach (var foodName in foodNames)
        {
            var path = $"Assets/ScriptableObjects/FoodData/FoodData_{foodName}.asset";
            CheckAndLoadFoodData(path, foodName);
        }
        
        //load saved ranking data
        rankingData = ES3.Load(RankingDataSaveKey, new Dictionary<string, int>());
    }

    private async void CheckAndLoadFoodData(string path, string foodName)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(path).Task;
        if (locations.Count > 0)
        {
            FoodData data = await Addressables.LoadAssetAsync<FoodData>(path).Task;
            if (data != null)
            {
                FoodName parsedFoodName;
                if (Enum.TryParse<FoodName>(foodName, out parsedFoodName))
                {
                    FoodNameToConfigs.Add(parsedFoodName, data);
                }
            }
        }
    }

    public void SaveScore()
    {
        
    }

    public Dictionary<string, int> LoadScore()
    {
        return null;
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
