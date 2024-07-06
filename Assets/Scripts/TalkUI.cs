using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class TalkUI : MonoBehaviour
{
    public TMP_Text text;
    public float typingSpeed = 0.05f;

    [Button]
    public void StartTyping(string str)
    {
        StartCoroutine(TypeText(str));
    }

    private IEnumerator TypeText(string str)
    {
        text.text = "";
        foreach (char c in str)
        {
            text.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}