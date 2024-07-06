using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodConfig", menuName = "ScriptableObjects/FoodConfig", order = 1)]
public class FoodConfig : ScriptableObject
{
    public Sprite[] DegradeSprites;
    public CellsInfo<bool> OccupyCells;
}
