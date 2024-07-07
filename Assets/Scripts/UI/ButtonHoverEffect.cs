using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.1f;  // 放大到1.1倍
    public float animationTime = 0.2f;  // 动画时长

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;  // 保存原始缩放
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 鼠标悬停时放大
        transform.DOScale(originalScale * hoverScale, animationTime).SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 鼠标离开时缩小
        transform.DOScale(originalScale, animationTime).SetEase(Ease.OutQuad);
    }
}