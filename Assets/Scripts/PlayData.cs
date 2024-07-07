using System.Collections;
using System.Collections.Generic;
using Battle;
using UnityEngine;

public class PlayData
{
    public int Score;
    public Dictionary<FoodName, int> EatenFood = new Dictionary<FoodName, int>();
    public int mergedFoodCount;
}
