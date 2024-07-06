using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class FoodData
    {
        public EmFoodType foodType;
        [ShowInInspector] public CellsInfo<bool> GridConfig;
        [PreviewField] public Sprite stage1;
        [PreviewField] public Sprite stage2;
    }

    public enum EmFoodType
    {
        Test1
    }

    [CreateAssetMenu(fileName = "FoodData", menuName = "Data/FoodData", order = 1)]
    public class FoodDataInfo : ScriptableObject
    {
        public List<FoodData> foodDataList;

        public FoodData GetFoodDataByType(EmFoodType foodType)
        {
            return foodDataList.FirstOrDefault(data => data.foodType == foodType);
        }
    }
}