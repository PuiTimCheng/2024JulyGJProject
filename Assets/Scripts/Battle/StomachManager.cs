using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class StomachManager : SerializedMonoBehaviour
    {
        [SerializeField] [ShowInInspector]
        private bool[,] _gridConfig = new bool[7, 7]; 

        public static StomachManager Instance { get; private set; }

        private GridLayoutGroup _gridLayoutGroup;
    
        public CellsInfo<global::Cell> Cells { get; private set; }

        public global::Cell CurrentSelecting { get; private set; }
        public GameObject cellPrefab;


        private void Awake()
        {
            Instance = this;

            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        
            _gridLayoutGroup.cellSize = new Vector2(87, 87);
            _gridLayoutGroup.spacing = new Vector2(5, 5); 
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = _gridConfig.GetLength(1);
            
            GenerateCells();
        }

        [Button]
        public void GenerateCells()
        {
            foreach (Transform child in _gridLayoutGroup.transform)
            {
                Destroy(child.gameObject);
            }

            Cells = new CellsInfo<global::Cell>(_gridConfig.GetLength(0), _gridConfig.GetLength(1), (cor) =>
            {
                var cell = Instantiate(cellPrefab, _gridLayoutGroup.transform).GetComponent<global::Cell>();
                cell.Init(cor, _gridConfig[cor.x, cor.y] ? CellState.Empty : CellState.Inactive, HighLightType.None);
                return cell;
            });
        }
        public void SelectCell(global::Cell cell)
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
}