using System.Collections.Generic;
using Battle;
using Sirenix.OdinInspector;
using TimToolBox.Extensions;
using UI.GameCanvasUIManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite[] randomSprites; 
    
    [SerializeField] Image _raycastImage; // For raycast only
    [SerializeField] Image _stateImage;
    [SerializeField] Image _highLightImage;

    Dictionary<HighLightType, Sprite> _highLightSpriteDic;
    Dictionary<CellState, Sprite> _stateSpriteDic;

    [ShowInInspector]public Vector2Int Index { get; private set; }
    public CellState CellState { get; private set; }
    public HighLightType HighLightType { get; private set; }

    public Food food;
    
    public void Init(Vector2Int index, CellState state, HighLightType highLightType)
    {
        Index = index;
        _stateImage.sprite = randomSprites.RandomPickOne();
        SetCellState(state);
        SetHighLightType(highLightType);
    }

    public void SetCellState(CellState cellState)
    {
        CellState = cellState;

        // TODO: Since i don't have art assets, use colour for now
        // _stateImage.sprite = _stateSpriteDic[cellState];
        _stateImage.color = cellState switch
        {
            CellState.Empty => new Color(1, 1, 1, 1),
            CellState.Inactive => new Color(1, 1, 1, 0),
            _ => new Color(1, 1, 1, 0.5f)
        };

        _raycastImage.raycastTarget = CellState != CellState.Inactive;
    }

    public void SetHighLightType(HighLightType highLightType)
    {
        if (CellState == CellState.Inactive) return;

        HighLightType = highLightType;

        // TODO: Since i don't have art assets, use colour for now

        // _highLightImage.sprite = _highLightSpriteDic[highLightType];
        _highLightImage.color = highLightType switch
        {
            HighLightType.None => new Color(1, 1, 1, 0),
            HighLightType.Valid => new Color(0, 1, 0, 0.5f),
            HighLightType.Invalid => new Color(1, 0, 0, 0.5f),
            _ => new Color(1, 1, 1, 0.5f)
        };
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StomachManager.Instance.SelectCell(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StomachManager.Instance.DeselectCell();
    }

    public void SetFood(Food obj)
    {
        food = obj;
    }
    
    public bool CanCombineWithNeighbors()
    {
        if (CellState != CellState.Occupied || food == null)
            return false;
        
        if (food.foodName == FoodName.Phone)
        {
            Vector2Int rightPosition = new Vector2Int(Index.x + 1, Index.y);
            if (StomachManager.Instance.Cells.IsValidPosition(rightPosition))
            {
                var rightCell = StomachManager.Instance.Cells.GetItem(rightPosition);
                if (rightCell != null && rightCell.CellState == CellState.Occupied &&
                    rightCell.food != null && rightCell.food.foodName == FoodName.Prawn)
                {
                    // 更新Sprite，不销毁Cell
                    food._img.sprite = GameCanvasUIManager.Instance.phone2;
                    rightCell.food._img.sprite = GameCanvasUIManager.Instance.prawn2;// 同上
                    GameCanvasUIManager.Instance.uIEffectManager.Burst(food.transform.localPosition, FoodName.PrawnAndPhone);
                    return true;
                }
            }
        }

        List<Vector2Int> neighborsPositions = new List<Vector2Int>
        {
            new Vector2Int(Index.x, Index.y + 1), // 上
            new Vector2Int(Index.x, Index.y - 1), // 下
            new Vector2Int(Index.x + 1, Index.y), // 右
            new Vector2Int(Index.x - 1, Index.y)  // 左
        };

        foreach (var pos in neighborsPositions)
        {
            if (!StomachManager.Instance.Cells.IsValidPosition(pos)) continue;
            var neighborCell = StomachManager.Instance.Cells.GetItem(pos);
            if (neighborCell == null || neighborCell.CellState != CellState.Occupied ||
                neighborCell.food == null) continue;
            if (GameManager.Level2Foods.Contains(neighborCell.food.foodName) || 
                GameManager.Level2Foods.Contains(food.foodName)) continue;
            
            foreach (var formula in food._data.foodFormulas)
            {
                if (formula.combineFood == neighborCell.food.foodName)
                {
                    StomachManager.Instance.Cells.DestroyCellByType(formula.combineFood);
                    food.UpgradeFood(formula.finalFood, formula.finalFoodSprite);
                    GameCanvasUIManager.Instance.tea.AddEnergy();
                    GameCanvasUIManager.Instance.uIEffectManager.Burst(food.transform.localPosition, formula.finalFood);
                    AudioManager.Instance.PlaySFX(SFXType.Merge);
                    PlaySceneController.Instance.AddMergeCount();
                    return true;
                }
            }
        }

        return false;
    }
}