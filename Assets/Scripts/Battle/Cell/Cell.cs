using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Cell : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image _raycastImage; // For raycast only
    [SerializeField] Image _stateImage;
    [SerializeField] Image _highLightImage;

    [SerializeField] Dictionary<HighLightType, Sprite> _highLightSpriteDic;
    [SerializeField] Dictionary<CellState, Sprite> _stateSpriteDic;

    public Vector2Int Index { get; private set; }
    public CellState CellState { get; private set; }
    public HighLightType HighLightType { get; private set; }

    public void Init(Vector2Int index, CellState state, HighLightType highLightType)
    {
        Index = index;
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
            CellState.Empty => new Color(1,1,1,0.2f),
            CellState.Inactive => new Color(1,1,1,0),
            _ => new Color(1,1,1,0.5f)
        };
        
        _raycastImage.raycastTarget = CellState != CellState.Inactive;
    }

    public void SetHighLightType(HighLightType highLightType)
    {
        if(CellState == CellState.Inactive) return;
        
        HighLightType = highLightType;
        
        // TODO: Since i don't have art assets, use colour for now

        // _highLightImage.sprite = _highLightSpriteDic[highLightType];
        _highLightImage.color = highLightType switch
        {
            HighLightType.None => new Color(1,1,1,0),
            HighLightType.Valid => new Color(0,1,0,0.5f),
            HighLightType.Invalid => new Color(1,0,0,0.5f),
            _ => new Color(1,1,1,0.5f)
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
}