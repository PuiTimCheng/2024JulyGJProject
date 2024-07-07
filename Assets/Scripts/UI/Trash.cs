using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Trash : MonoBehaviour
    {
        public TMP_Text text;
        private Sequence _textSequence;
        
        public void ShowText()
        {
            if (_textSequence != null && _textSequence.IsActive())
            {
                _textSequence.Kill();
            }
            
            text.gameObject.SetActive(true);
            _textSequence = DOTween.Sequence();
            _textSequence.Append(text.transform.DOScale(1.2f, 0.3f)) // 在0.3秒内放大到1.2倍
                .Append(text.transform.DOScale(1f, 0.1f)) // 然后在0.1秒内缩回到1倍
                .AppendInterval(1f) // 等待1秒
                .OnComplete(() => text.gameObject.SetActive(false));
            
            _textSequence.Play();
        }
    }
}