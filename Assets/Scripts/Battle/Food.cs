using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Battle
{
    public class Food : SerializedMonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private bool[,] _gridConfig = new bool[4, 4];
        private Image _img;
        private List<Vector2Int> _parentCells;
        public CellsInfo<bool> Orientation;
        public FoodDataInfo foodDataInfo;
        
        
        [Button]
        public void TestCreateFood()
        {
            InitFood(foodDataInfo.GetFoodDataByType(EmFoodType.Test1));
        }

        private void Awake()
        {
            _img = GetComponent<Image>();
        }

        public void InitFood(FoodData data)
        {
            // _gridConfig = data.GridConfig;
            _img.sprite = data.stage1;
            
            ConvertGridConfigToCellsInfo();
        }
        
        private void ConvertGridConfigToCellsInfo()
        {
            var width = _gridConfig.GetLength(0);
            var height = _gridConfig.GetLength(1);

            Orientation = new CellsInfo<bool>(width, height, cor => _gridConfig[cor.x, cor.y]);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            FoodManager.Instance.StartDrag(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Cancel();
        }

        private void Cancel()
        {
            SetRaycastAble(true);
        }

        public void SetRaycastAble(bool value)
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
    }
}