using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UI.GameCanvasUIManager;

namespace Battle
{
    public class Food : SerializedMonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] public Image _img;
        private List<Vector2Int> _parentCells;
        public CellsInfo<bool> Orientation => _data.GridConfig ?? null;
        private Plate _fromPlate;
        private Sequence _selectEffectSequence;
        
        public FoodData _data;
        float _curTime;
        int _curPhase;
        bool _digesting;

        public IEnumerator BeginDigest()
        {
            yield return new WaitForSeconds(6);
            GameCanvasUIManager.Instance.uIEffectManager.Poison(GetComponent<RectTransform>().anchoredPosition);
            ShowSelectEffect();
            yield return new WaitForSeconds(4);
            HideSelectEffect();
            OnDigest();
        }
        
        public void ShowSelectEffect()
        {
            _selectEffectSequence?.Kill(); 

            var originalColor = _img.color;
            var targetColor = Color.red;

            _selectEffectSequence = DOTween.Sequence();
            _selectEffectSequence.Append(_img.DOColor(targetColor, 0.7f).SetEase(Ease.InOutFlash));
            _selectEffectSequence.Append(_img.DOColor(originalColor, 0.7f).SetEase(Ease.InOutFlash));
            _selectEffectSequence.SetLoops(-1, LoopType.Yoyo);
        }

        public void HideSelectEffect()
        {
            _selectEffectSequence?.Kill();
            _img.color = Color.white;
        }

        public void InitFood(FoodData data)
        {
            _data = data;
            _img.sprite = data.stage1;
            _img.SetNativeSize();
            _fromPlate = transform.parent.GetComponent<Plate>();
        }

        public void UpgradeFood(FoodName name, Sprite sprite)
        {
            _data.foodName = name;
            _img.sprite = sprite;
            HideSelectEffect();
            StopAllCoroutines();
            StartDigest();
        }

        public void StartDigest()
        {
            Debug.Log($"Start Disgest ");
            _img.raycastTarget = false;
            _curTime = 0;
            _curPhase = 0;
            _digesting = true;
            StartCoroutine(BeginDigest());
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
            
            // 清除可能正在运行的 Tween 动画
            HideSelectEffect();

            // 停止消化的协程
            StopAllCoroutines();
            
            GameCanvasUIManager.Instance.iconAnimation.StartIconAnimation(10, transform.position);
            PlaySceneController.Instance.AddScore(GameManager.GetFoodScore(_data.foodName));
            PlaySceneController.Instance.AddEatenDish(_data.foodName);
            StomachManager.Instance.SetCellState(_parentCells.Select(_ => (_, CellState.Empty)).ToList());
            AudioManager.Instance.PlaySFX(SFXType.Digest);
            OnDiscard(); // TEMP    
        }

        // This called when release the mouse on a invalid cell or outside the stomach
        public void OnDiscard()
        {
            Destroy(gameObject);
        }
    }
}
