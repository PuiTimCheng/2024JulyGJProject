using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultEntryUI : MonoBehaviour
{
    public TMP_Text foodNameText;
    public TMP_Text countText;
    public TMP_Text priceText;

    public void SetText(string foodName, string count, string price)
    {
        foodNameText.text = foodName;
        countText.text = count;
        priceText.text = price;
    }
}
