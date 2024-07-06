using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodConfig", menuName = "ScriptableObjects/FoodConfig", order = 1)]
public class FoodConfig : ScriptableObject
{
    public Sprite[] DegradeSprites;
    
    [ShowInInspector]
    [TableMatrix(HorizontalTitle = "Cells", SquareCells = true)]
    public bool[,] CellLayout = new bool[4,4];
}
