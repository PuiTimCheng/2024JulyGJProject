using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StomachManager : SerializedMonoBehaviour
{
    public static StomachManager Instance { get; private set; }

    public GridLayoutGroup GridLayoutGroup;
    
    public CellsInfo<Cell> Cells { get; private set; }

    public Cell CurrentSelecting { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public GameObject _cellPrefab;

    public void GenerateCells(CellsInfo<bool> source)
    {
        GridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        GridLayoutGroup.constraintCount = source.Width;
        
        Cells = new CellsInfo<Cell>(source.Width, source.Height, (cor) =>
        {
            var cell = Instantiate(_cellPrefab, GridLayoutGroup.transform).GetComponent<Cell>();
            cell.Init(cor, source.GetItem(cor) ? CellState.Empty : CellState.Inactive, HighLightType.None);
            return cell;
        });
    }
    public void SelectCell(Cell cell)
    {
        CurrentSelecting = cell;
    }
    public void DeselectCell()
    {
        CurrentSelecting = null;
    }

    public void ClearHighLight()
    {
        foreach (var cell in Cells.Cells)
        {
            cell.SetHighLightType(HighLightType.None);
        }
    }
    
    public void SetHighLight(List<(Vector2Int, HighLightType)> highLightList)
    {
        foreach (var (cor, highLightType) in highLightList)
        {
            Cells.GetItem(cor).SetHighLightType(highLightType);
        }
    }

    public void SetCellState(List<(Vector2Int, CellState)> cellStateList)
    {
        foreach (var (cor, state) in cellStateList)
        {
            Cells.GetItem(cor).SetCellState(state);
        }
    }
}