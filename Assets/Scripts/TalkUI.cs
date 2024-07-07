using System.Collections;
using System.Collections.Generic;
using DG.Tweening; // 引入DoTween
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class TalkUI : MonoBehaviour
{
    public TMP_Text text;
    public float typingSpeed = 0.05f;
    public RectTransform textTransform; // 添加一个用于控制动画的RectTransform
    public List<string> sentences = new List<string>
    {
        "从来没有人在我这家店吃回本过",
        "希望你的胃口和你的口气一样大",
        "实在不行去吐一下",
        "要不要多交40块钱，顺便帮你把肠胃科的号给挂了",
        "现在认输钱退一半",
        "上一个在我店里这么自信的小伙子，是从我店里爬出去的",
        "小鸟胃?",
        "不能吃就戒了"
    }; // 用于存储对话的列表

    [Button]
    public void StartDialogue()
    {
        textTransform.localScale = Vector3.zero; // 初始时隐藏
        textTransform.DOScale(1, 0.5f) // DoTween动画，缩放至正常大小
            .OnComplete(() => StartCoroutine(GetRandomSentence()));
    }

    private IEnumerator TypeText(string str)
    {
        text.text = "";
        foreach (char c in str)
        {
            text.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        // 打字结束后缩回
        textTransform.DOScale(0, 0.5f);
    }

    private string GetRandomSentence()
    {
        return sentences[Random.Range(0, sentences.Count)]; // 从列表中随机选一个字符串
    }
}