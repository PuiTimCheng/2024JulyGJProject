using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayTimerUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public Slider slider;
    public RectTransform fireRect;
    public void UpdateTime(float timeInSecond)
    {
        //convert timeInSecond to display in a format of mm:ss
        timeText.text = string.Format("{0:00}:{1:00}", (int)timeInSecond / 60, (int)timeInSecond % 60);
    }
    public void UpdateTimeRatio(float ratio)
    {
        slider.value = Mathf.Lerp(0.05f,1,ratio);
        UpdateFirePosition(slider.value);
    }
    
    private void UpdateFirePosition(float value)
    {
        float startPosition = -650f;
        float endPosition = 650f;
        float newPosition = Mathf.Lerp(startPosition, endPosition, value);
        fireRect.anchoredPosition = new Vector3(fireRect.anchoredPosition.x, newPosition, 0);
    }
}
