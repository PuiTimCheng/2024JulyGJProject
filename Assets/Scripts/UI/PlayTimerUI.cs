using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayTimerUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public Slider slider;
    public void UpdateTime(float timeInSecond)
    {
        //convert timeInSecond to display in a format of mm:ss
        timeText.text = string.Format("{0:00}:{1:00}", (int)timeInSecond / 60, (int)timeInSecond % 60);
    }
    public void UpdateTimeRatio(float ratio)
    {
        slider.value = Mathf.Lerp(0.05f,1,ratio);
    }
}
