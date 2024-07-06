using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Battle
{
    public class Food : SerializedMonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _img;
        private List<Vector2Int> _parentCells;
        public CellsInfo<bool> Orientation => _data.GridConfig ?? null;
        FoodData _data;

        public void InitFood(FoodData data)
        {
            _data = data;
            _img.sprite = data.stage1;
            _img.SetNativeSize();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            FoodManager.Instance.StartDrag(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
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

        // This called when release the mouse on a invalid cell or outside the stomach
        public void OnDiscard()
        {
            Destroy(gameObject);
        }
    }
}


public enum FoodName
{
    Beef,
    Cake,
    Egg,
    Noodle,
    Prawn,
    Rice,
    Sausage,
    WatermelonSlice
}
