using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TimToolBox.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class ComicManager : MonoBehaviour
{
    public List<RectTransform> Pages;
    public List<Image> Page1Image;
    public List<Image> Page2Image;
    public List<Image> Page3Image;
    public List<Image> Page4Image;

    public int currentPageNum;
    public bool canNext;
    public int[] pageSteps;
    public bool[] pageDone;

    private void Start()
    {
        pageSteps = new int[4];
        pageDone = new bool[4];
        foreach (var page in Pages)
        {
            page.gameObject.SetActive(false);
        }
        foreach (var img in Page1Image)
        {
            img.color = img.color.SetAlpha(0);
        }
        foreach (var img in Page2Image)
        {
            img.color = img.color.SetAlpha(0);
        }
        foreach (var img in Page3Image)
        {
            img.color = img.color.SetAlpha(0);
        }
        foreach (var img in Page4Image)
        {
            img.color = img.color.SetAlpha(0);
        }

        Pages[0].gameObject.SetActive(true);
        canNext = true;
        Next();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Next();
        }
    }

    public void Next()
    {
        if (canNext)
        {
            canNext = false;
            if (pageDone[currentPageNum])
            {
                currentPageNum++;
                if (currentPageNum >= Pages.Count) GameManager.Instance.LoadPlayScene();
                else Pages[currentPageNum].gameObject.SetActive(true);
            }
            switch (currentPageNum)
            {
                case 0:
                    Page1Next();
                    break;
                case 1:
                    Page2Next();
                    break;
                case 2:
                    Page3Next();
                    break;
                case 3:
                    Page4Next();
                    break;
            }
        }
    }

    public void Page1Next()
    {
        switch (pageSteps[0])
        {
            case 0:
                Page1Image[0].DOFade(1, 0.5f).OnComplete(() => canNext = true);
                break;
            case 1: 
                Page1Image[1].DOFade(1, 0.5f).OnComplete(() => canNext = true);
                break;
            case 2: 
                Page1Image[2].DOFade(1, 0.5f).OnComplete(() => canNext = true);
                break;
            case 3: 
                Page1Image[3].DOFade(1, 0.5f).OnComplete(() =>
                {
                    pageDone[0] = true;
                    canNext = true;
                });
                break;
        }
        pageSteps[0]++;
    }
    public void Page2Next()
    {
        switch (pageSteps[1])
        {
            case 0:
                Page2Image[0].DOFade(1, 0.5f);
                Page2Image[0].GetComponent<RectTransform>().DOShakePosition(0.5f,20).OnComplete(() => canNext = true);
                break;
            case 1: 
                Page2Image[1].DOFade(1, 0.5f).OnComplete(() => canNext = true);
                break;
            case 2: 
                Page2Image[2].DOFade(1, 0.5f).OnComplete(() =>
                {
                    pageDone[1] = true;
                    canNext = true;
                });
                break;
        }
        pageSteps[1]++;
    }
    public void Page3Next()
    {
        switch (pageSteps[2])
        {
            case 0:
                Page3Image[0].DOFade(1, 0.2f);//.OnComplete(() => canNext = true);
                Page3Image[0].GetComponent<RectTransform>().DOShakePosition(1,50).OnComplete(() => canNext = true);
                break;
            case 1: 
                Sequence sequence = DOTween.Sequence();
                sequence.Append(Page3Image[1].DOFade(1, 0.3f));
                sequence.Append(Page3Image[2].DOFade(1, 0.3f));
                sequence.Append(Page3Image[3].GetComponent<RectTransform>().DOShakePosition(0.5f, 50).OnComplete(() =>
                {
                    pageDone[2] = true;
                    canNext = true;
                }));
                sequence.Join(Page3Image[3].DOFade(1, 0.2f));
                sequence.Play();
                break;
        }
        pageSteps[2]++;
    }
    public void Page4Next()
    {
        switch (pageSteps[3])
        {
            case 0:
                Page4Image[0].DOFade(1, 1f).OnComplete(() =>
                {
                    pageDone[3] = true;
                    canNext = true;
                });
                break;
        }
        pageSteps[3]++;
    }
}
