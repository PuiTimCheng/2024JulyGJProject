using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingEntryUI : MonoBehaviour
{
    public TMP_Text rankText;
    public TMP_Text nameText;
    public TMP_Text scoreText;

    public void SetText(string rank, string name, string score)
    {
        rankText.text = rank;
        nameText.text = name;
        scoreText.text = score;
    }
}
