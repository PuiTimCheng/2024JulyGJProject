using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingUI : MonoBehaviour, IUIPanel
{
    public List<RankingEntryUI> rankEntries;

    public void SetRankFromSave()
    {
        ES3.Load(GameManager.RankingDataSaveKey, new Dictionary<string, int>());
    }
    
    public void Show()
    {
        
    }

    public void Hide()
    {
    }
}
