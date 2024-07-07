using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreNumText;

    public void UpdateScore(int score)
    {
        //convert timeInSecond to display in a format of mm:ss
        scoreNumText.text = score.ToString();
    }
}
