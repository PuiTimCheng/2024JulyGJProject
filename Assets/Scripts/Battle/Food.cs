using System;
using System.Collections.Generic;
using System.Linq;
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
        float _curTime;
        int _curPhase;
        bool _digesting;
        
        void Update()
        {
            if (_digesting)
            {
                _curTime += Time.deltaTime;

                switch (_curPhase)
                {
                    case 0:
                        if (_curTime >= _data.Phase0Time)
                        {
                            _curPhase++;
                            _img.sprite = _data.stage2;
                            // TODO: Add Cell VFX?
                        }
                        break;
                    case 1:
                        if (_curTime >= _data.Phase1Time)
                        {
                            OnDigest();
                        }
                        break;
                }
            }
        }

        public void InitFood(FoodData data)
        {
            _data = data;
            _img.sprite = data.stage1;
        }

        public void StartDigest()
        {
            _curTime = 0;
            _curPhase = 0;
            _digesting = true;
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

        public void OnDigest()
        {
            _digesting = false;
            StomachManager.Instance.SetCellState(_parentCells.Select(_ => (_, CellState.Empty)).ToList());
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
    Noodle,
    //Sausage,
    //Tempura,
    //Watermelon,
    //Beef,
    //PizzaSlice,
    //Shrimp,
    //Egg,
    //Rice,
    //Potato,
    //Fish,
    //Broccoli,
    //Seaweed,
    //Bread,
    //Mushroom,
    //Tomato,
    //Chicken,
}
