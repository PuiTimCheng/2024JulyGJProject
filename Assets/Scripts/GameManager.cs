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
    [SerializeField] MMF_Player loadComicScenePlayer;
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
        LoadScore();
    }

    private async void CheckAndLoadFoodData(string path, string foodName)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(path).Task;
        if (locations.Count > 0)
        {
            var data = await Addressables.LoadAssetAsync<FoodData>(path).Task;
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

    public void SaveScore(Dictionary<string, int> dict)
    {
        ES3.Save(GameManager.RankingDataSaveKey, dict);
    }

    public Dictionary<string, int> LoadScore()
    {
        rankingData = ES3.Load(RankingDataSaveKey, new Dictionary<string, int>());
        return rankingData;
    }
    
    public void LoadPlayScene()
    {
        loadPlayScenePlayer.Initialization();
        loadPlayScenePlayer.PlayFeedbacks();
    }

    public void LoadComicScene()
    {
        AudioManager.Instance.PlayBGM(BGMType.Intro);
        loadComicScenePlayer.Initialization();
        loadComicScenePlayer.PlayFeedbacks();
    }
    
    public void LoadMenuScene()
    {
        loadMenuScenePlayer.Initialization();
        loadMenuScenePlayer.PlayFeedbacks();
    }
    
    public void ShowRanking()
    {
        RankingUI.Instance.Show();
    }

    public void ShowCredits()
    {
        
    }
    
    
    public static int GetFoodScore(FoodName food)
    {
        switch (food)
        {
            case FoodName.RiceAndBeef:
                return 500;
            case FoodName.RiceAndEgg:
                return 500;
            case FoodName.ShrimpAndBiscuit:
                return 500;
            case FoodName.WatermelonAndRice:
                return 500;
            case FoodName.EggAndBiscuit:
                return 500;
            case FoodName.SausageAndBread:
                return 500;
            case FoodName.ShrimpAndNoodle:
                return 500;
            case FoodName.BeefAndBread:
                return 500;
            case FoodName.TomatoAndFish:
                return 500;
            case FoodName.BreadAndChicken:
                return 500;
            case FoodName.ChickenAndBiscuit:
                return 500;
            default:
                return GameManager.Instance.FoodNameToConfigs[food].foodScore;
        }
    }

}
