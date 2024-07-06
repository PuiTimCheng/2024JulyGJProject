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
        [SerializeField] public Image _img;
        private List<Vector2Int> _parentCells;
        public CellsInfo<bool> Orientation => _data.GridConfig ?? null;
        private Plate _fromPlate;
        
        public FoodData _data;
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
                        //if (_curTime >= _data.Phase0Time)
                        if (_curTime >= 8)
                        {
                            _curPhase++;
                            // TODO: Add Cell VFX?
                        }
                        break;
                    case 1:
                        //if (_curTime >= _data.Phase1Time)
                        if (_curTime >= 10)
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
            _img.SetNativeSize();
            _fromPlate = transform.parent.GetComponent<Plate>();
        }

        public void StartDigest()
        {
            Debug.Log($"Start Disgest ");
            _img.raycastTarget = false;
            _curTime = 0;
            _curPhase = 0;
            _digesting = true;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            FoodManager.Instance.StartDrag(this);
            _fromPlate?.ClearCellPresenter();
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
            //PlaySceneController.Instance.AddScore(_data.foodScore);
            PlaySceneController.Instance.AddScore(100);
            StomachManager.Instance.SetCellState(_parentCells.Select(_ => (_, CellState.Empty)).ToList());
            OnDiscard(); // TEMP
        }

        // This called when release the mouse on a invalid cell or outside the stomach
        public void OnDiscard()
        {
            Destroy(gameObject);
        }
    }
}
