using Battle;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public class StartAndEndText : MonoBehaviour
    {
        [Button]
        public void ShowTea()
        {
            var rect = GetComponent<RectTransform>();
            DOTween.Sequence()
                .Append(rect.DOAnchorPosX(0, 0.5f).SetUpdate(true).SetEase(Ease.InOutQuad)) // 添加渐变效果，平滑地移入
                .AppendInterval(1f) // 中间停留0.4秒
                .Append(
                    rect.DOAnchorPosX(1960, 0.5f).SetUpdate(true).SetEase(Ease.InOutQuad)) // 添加渐变效果，平滑地移出
                .OnComplete(() =>
                {
                    rect.anchoredPosition = new Vector3(-1960, 0, 0); // 确保重新定位到起始位置
                    StomachManager.Instance.Cells.DestroyAllFood(); // 调用适当的清理方法
                })
                .Play();
        }
    }
}