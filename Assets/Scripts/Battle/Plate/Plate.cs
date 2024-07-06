using System.Collections;
using System.Collections.Generic;
using Battle;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    [SerializeField] Food _food;
    [SerializeField] CellPresenter _cellPresenter;
    [SerializeField] Image _plate;
    
    public void Init(FoodData food)
    {
        _food.InitFood(food);
        _cellPresenter.GenerateCell(food);
        //TODO: Set plate sprite
    }
}
