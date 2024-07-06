using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIEffectManager : MonoBehaviour
{
    public ParticleSystem BurstObject;
    public TMP_Text text;
    private Sequence _textSequence;

    public void Burst(Vector3 pos)
    {
        var effect = Instantiate(BurstObject, transform);
        effect.Play();
        effect.GetComponent<RectTransform>().anchoredPosition = pos;
        text.rectTransform.anchoredPosition = pos;
        
        ShowText(pos);
    }

    public void ShowText(Vector3 pos)
    {
        text.gameObject.SetActive(true);
        
        _textSequence = DOTween.Sequence();
        _textSequence.Append(text.transform.DOScale(1.2f, 0.3f)) // 在0.1秒内放大到1.2倍
            .Append(text.transform.DOScale(1f, 0.1f)) // 然后在0.1秒内缩回到1倍
            .AppendInterval(1f) // 等待1秒
            .OnComplete(() => text.gameObject.SetActive(false));
    }
}
