using System.Linq;
using TimToolBox.Extensions;
using UI.GameCanvasUIManager;
using UnityEngine;

namespace Battle
{
    public class FoodManager : MonoBehaviour
    {
        public static FoodManager Instance;

        public Food _curDraggingFood { get; private set; }

        void Awake()
        {
            Instance = this;
        }

        public void StartDrag(Food draggable)
        {
            Debug.Log("StartDrag");
            if (_curDraggingFood)
            {
                Debug.LogError("Current is dragging. ");
                return;
            }

            _curDraggingFood = draggable;
            _curDraggingFood.SetRaycastAble(false);
            _curDraggingFood.transform.SetParent(transform);
            _curDraggingFood.GetComponent<RectTransform>().localScale = Vector3.one * StomachManager.Instance.rectTransform.localScale.x;
            
            AudioManager.Instance.PlaySFX(SFXType.Drag);
        }

        private void Update()
        {
            if (!_curDraggingFood) return;
            StomachManager.Instance.ClearHighLight();

            // TODO: switch to new inputsystem if necessary
            if (Input.GetMouseButton(0))
            {
                OnMouseHold();
            }
            else
            {
                OnMouseUp();
            }
        }
        
        void OnMouseHold()
        {
            if (StomachManager.Instance.CurrentSelecting != null &&
                StomachManager.Instance.CurrentSelecting.CellState != CellState.Inactive)
            {
                var stomach = StomachManager.Instance.Cells;

                var canPlace = CellsExtensions.CanPlaced(
                    _curDraggingFood.Orientation,
                    StomachManager.Instance.Cells,
                    StomachManager.Instance.CurrentSelecting.Index,
                    out var eligibleCells, out var allCells, out var inBound);

                StomachManager.Instance.SetHighLight(eligibleCells
                    .Select(_ => (_, canPlace ? HighLightType.Valid : HighLightType.Invalid)).ToList());

                if (inBound)
                {
                    var pos = Vector3.zero;
                    foreach (var cor in allCells)
                    {
                        pos += stomach.GetItem(cor).GetComponent<RectTransform>().position;
                    }
                    pos /= allCells.Count;

                    _curDraggingFood.GetComponent<RectTransform>().position = pos;
                }
                else
                {
                    _curDraggingFood.GetComponent<RectTransform>().position = Camera.main.ScreenToWorldPoint(Input.mousePosition).Set(z:0);
                }
                
                /*if (canPlace)
                {
                    var pos = Vector3.zero;

                    foreach (var cor in eligibleCells)
                    {
                        pos += stomach.GetItem(cor).transform.position;
                    }

                    pos /= eligibleCells.Count;

                    _curDraggingFood.transform.position = pos;
                }*/
            }
            else
            {
                _curDraggingFood.GetComponent<RectTransform>().position = Camera.main.ScreenToWorldPoint(Input.mousePosition).Set(z:0);
            }
        }

        void OnMouseUp()
        {
            // Basically is on mouse up
            if (StomachManager.Instance.CurrentSelecting != null &&
                StomachManager.Instance.CurrentSelecting.CellState != CellState.Inactive)
            {
                var canPlace = CellsExtensions.CanPlaced(
                    _curDraggingFood.Orientation,
                    StomachManager.Instance.Cells,
                    StomachManager.Instance.CurrentSelecting.Index,
                    out var eligibleCells, out var allCells, out var allInBound);

                var stomach = StomachManager.Instance.Cells;
                if (canPlace && allInBound)
                {
                    var pos = Vector3.zero;
                    foreach (var cor in allCells)
                    {
                        pos += stomach.GetItem(cor).GetComponent<RectTransform>().position;
                    }
                    pos /= allCells.Count;
                    _curDraggingFood.GetComponent<RectTransform>().position = pos;
                    
                    /*var pos = Vector3.zero;

                    foreach (var cor in eligibleCells)
                    {
                        pos += stomach.GetItem(cor).transform.position;
                    }
                    pos /= eligibleCells.Count;*/
                    
                    _curDraggingFood.StartDigest();

                    _curDraggingFood.SetParentCells(eligibleCells);


                    StomachManager.Instance.SetCellState(eligibleCells
                        .Select(_ => (_, canPlace ? CellState.Occupied : CellState.Empty)).ToList(), _curDraggingFood);
                    _curDraggingFood = null;
                    
                    AudioManager.Instance.PlaySFX(SFXType.Place);

                }
                else
                {
                    // TODO: this happen when player release the mouse on a invalid cell or outside the stomach, not sure it should go back or what, I'll leave it Destroy for now.
                    _curDraggingFood.OnDiscard();
                    _curDraggingFood = null;
                    AudioManager.Instance.PlaySFX(SFXType.Trash);
                    AudioManager.Instance.PlaySFX(SFXType.Click);
                }
            }
            else
            {
                // TODO: this happen when player release the mouse on a invalid cell or outside the stomach, not sure it should go back or what, I'll leave it Destroy for now.
                _curDraggingFood.OnDiscard();
                GameCanvasUIManager.Instance.trash.ShowText();
                GameCanvasUIManager.Instance.uIEffectManager.Burst(_curDraggingFood.GetComponent<RectTransform>().anchoredPosition);
                _curDraggingFood = null;
                AudioManager.Instance.PlaySFX(SFXType.Trash);
                AudioManager.Instance.PlaySFX(SFXType.Click);
            }
        }

    }
}