using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This script should be put on the food prefab
/// </summary>
public class Food : SerializedMonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private Image _img;

    List<Vector2Int> _parentCells; // The actual cells that this food use in stomach. 

    public CellsInfo<bool> Orientation;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start Drag");
        FoodManager.Instance.StartDrag(this);
    }

    public void SetRaycastable(bool value)
    {
        if (_img)
        {
            _img.raycastTarget = value;
        }
    }

    public void SetParentCells(List<Vector2Int> cells)
    {
        _parentCells = cells;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}


public enum FoodName
{
    Noodle,
    Sausage,
    Tempura,
    Watermelon,
    Beef,
    PizzaSlice,
    Shrimp,
    Egg,
    Rice,
    Potato,
    Fish,
    Broccoli,
    Seaweed,
    Bread,
    Mushroom,
    Tomato,
    Chicken,
}
