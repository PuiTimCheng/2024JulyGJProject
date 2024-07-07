using System;
using Battle;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Tea : MonoBehaviour
    {
        public TMP_Text text;
        private Sequence _textSequence;
        public Image fillImage;
        public int energy;
        public GameObject effect;
        public GameObject space;

        public GameObject UseTeaUI;

        private void Update()
        {
            if (energy == 100 && Input.GetKey(KeyCode.Space))
            {
                UseTea();
            }
        }

        [Button]
        public void AddEnergy()
        {
            if (energy == 100) return;

            energy = Mathf.Clamp(energy + 25, 0, 100);
            float targetFillAmount = energy / 100f;

            if (energy == 100)
            {
                effect.SetActive(true);
                space.SetActive(true);
            }
            ShowText();
            fillImage.DOFillAmount(targetFillAmount, 0.2f); // 在0.2秒内改变fillAmount
        }

        [Button]
        public void UseTea()
        {
            float targetFillAmount = 0;
            fillImage.DOFillAmount(targetFillAmount, 0.2f); // 在0.2秒内改变fillAmount
            effect.SetActive(false);
            space.SetActive(false);
            
            UseTeaUI.SetActive(true);
            DOTween.Sequence()
                .Append(UseTeaUI.transform.DOScale(1.5f, 0.3f).SetUpdate(true)) // 即使在Time.timeScale = 0时也更新
                .Append(text.transform.DOScale(1f, 0.2f).SetUpdate(true))
                .OnComplete(() => {
                    UseTeaUI.SetActive(false);
                    StomachManager.Instance.Cells.DestroyAllFood();
                })
                .Play();
        }
        
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