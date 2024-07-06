using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TimToolBox.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RankingUI : MonoBehaviour, IUIPanel
{
    public static RankingUI _instance;

    public static RankingUI Instance
    {
        get
        {
            if (!_instance)
            {
                var prefab = Addressables.LoadAssetAsync<GameObject>("Assets/Prefab/RankingCanvas.prefab")
                    .WaitForCompletion();
                _instance = Instantiate(prefab).GetComponent<RankingUI>();
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }
    
    public RectTransform root;
    public Button closeBtn;
    public List<RankingEntryUI> rankEntriesUI;
    private void Awake()
    {
        rankEntriesUI = GetComponentsInChildren<RankingEntryUI>().ToList();
        closeBtn.onClick.AddListener(Hide);
    }

    public void SetRankFromSave()
    {
        var dict = GameManager.Instance.LoadScore();
        var rankEntriesData = dict.OrderByDescending(x => x.Value).ToList();
        var entryCount = rankEntriesUI.Count;
        for (int i = 0; i < entryCount; i++)
        {
            var entryUI = rankEntriesUI[i];
            if (i < rankEntriesData.Count)
            {
                var entry = rankEntriesData[i];
                entryUI.SetText($"{i+1}", entry.Key.ToString(), entry.Value.ToString());
            }
            else
            {
                entryUI.SetText($"{i+1}", "---", "---");
            }
        }
    }
    
    public void Show()
    {
        root.gameObject.SetActive(true);
        SetRankFromSave();
    }

    public void Hide()
    {
        root.gameObject.SetActive(false);
    }
}
