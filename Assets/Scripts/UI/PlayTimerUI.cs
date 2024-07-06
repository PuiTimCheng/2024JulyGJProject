using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayTimerUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    public void UpdateTime(float timeInSecond)
    {
        //convert timeInSecond to display in a format of mm:ss
        timeText.text = string.Format("{0:00}:{1:00}", (int)timeInSecond / 60, (int)timeInSecond % 60);
    }
}
