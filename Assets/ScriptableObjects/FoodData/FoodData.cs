using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Battle
{
    public enum FoodName
    {
        Beef,
        Biscuit, //饼干
        Bread,
        Broccoli, //西兰花
        Cake,
        Chicken,
        Egg,
        Fish,
        Mushroom,
        Noodle,
        Prawn,
        Rice,
        Stone,
        Tomato,
        WatermelonSlice,
        Shrimp,
        HotDog,
        Sausage,

        [LabelText("咖喱饭")] RiceAndBeef, //咖喱饭
        [LabelText("蛋炒饭")] RiceAndEgg, //蛋炒饭
        [LabelText("虾饺")] ShrimpAndBiscuit, //虾饺
        [LabelText("西瓜拌饭")] WatermelonAndRice, //西瓜拌饭
        [LabelText("蛋饺")] EggAndBiscuit, //蛋饺
        [LabelText("热狗")] SausageAndBread,
        [LabelText("海鲜面")] ShrimpAndNoodle,
        [LabelText("牛角面包")] BeefAndBread,
        [LabelText("番茄鱼")] TomatoAndFish,
        [LabelText("三明治")] BreadAndChicken,
        [LabelText("饺子")] ChickenAndBiscuit,
    }

    [Serializable]
    public class FoodFormula
    {
        [LabelText("可以合成的食物")] public FoodName combineFood;
        [LabelText("最终生成的食物")] public FoodName finalFood;
    }

    [Serializable]
    [CreateAssetMenu(fileName = "FoodData", menuName = "Data/FoodData", order = 1)]
    public class FoodData : SerializedScriptableObject
    {
        public FoodName foodName;
        public int foodScore;
        [ShowInInspector] public CellsInfo<bool> GridConfig;
        [PreviewField] public Sprite stage1;
        [PreviewField] public Sprite plateSprite;
        [SerializeField] public float Phase0Time;
        [SerializeField] public float Phase1Time;

        public List<FoodFormula> foodFormulas;
    }
}