using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using TimToolBox.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class CreditUI : MonoBehaviour, IUIPanel
{
    public Button closeBtn;
    public Image BG;
    public RectTransform Credit;

    public bool isShown;

    private void Start()
    {
        gameObject.SetActive(false);
        closeBtn.onClick.AddListener(Hide);
        
        isShown = false;
        BG.color = BG.color.SetAlpha(0);
        Credit.localScale = Vector3.zero;
    }

    public void Show()
    {
        if (!isShown)
        {
            isShown = true;
            gameObject.SetActive(true);
            BG.DOFade(1, 0.5f);
            Credit.DOScale(0.9f, 0.5f);
        }
        
    }

    public void Hide()
    {
        if (isShown)
        {
            isShown = false;
            BG.DOFade(0, 0.5f).OnComplete(() => {gameObject.SetActive(false);});
            Credit.DOScale(0, 0.5f);
        }

    }
}
