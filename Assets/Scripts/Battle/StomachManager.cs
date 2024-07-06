using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class StomachManager : SerializedMonoBehaviour
    {
        public static StomachManager Instance { get; private set; }
        private GridLayoutGroup _gridLayoutGroup;
        public CellsInfo<Cell> Cells { get; private set; }
        public Cell CurrentSelecting { get; private set; }
        public GameObject cellPrefab;

        public RectTransform rectTransform;
        // TEMP:
        public CellsInfo<bool> FakeStart;
        
        
        private void Awake()
        {
            Instance = this;
            
            rectTransform = GetComponent<RectTransform>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();

            _gridLayoutGroup.cellSize = new Vector2(87, 87);
            
            GenerateCells(FakeStart);
        }

        public void GenerateCells(CellsInfo<bool> source)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = source.Width;

            Cells = new CellsInfo<Cell>(source.Width, source.Height, (cor) =>
            {
                var cell = Instantiate(cellPrefab, _gridLayoutGroup.transform).GetComponent<Cell>();
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

        public void SetCellState(List<(Vector2Int, CellState)> cellStateList, Food food = null)
        {
            foreach (var (cor, state) in cellStateList)
            {
                Cells.GetItem(cor).SetCellState(state);
                Cells.GetItem(cor).SetFood(food);
            }

            Cells.CheckCombine();
        }
    }
}