using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Battle
{
    [Serializable]
    [CreateAssetMenu(fileName = "FoodData", menuName = "Data/FoodData", order = 1)]
    public class FoodData : SerializedScriptableObject
    {
        public int foodScore;
        [ShowInInspector] public CellsInfo<bool> GridConfig;
        [PreviewField] public Sprite stage1;
        [PreviewField] public Sprite stage2;
        [PreviewField] public Sprite plateSprite;
        [SerializeField] public float Phase0Time;
        [SerializeField] public float Phase1Time;
    }
}