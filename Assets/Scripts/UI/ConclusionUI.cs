using System;
using System.Linq;
using TimToolBox.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.GameCanvasUIManager
{
    public class ConclusionUI : MonoBehaviour, IUIPanel
    {
        public RectTransform contentRectTransform;
        public TextMeshProUGUI scoreText;
        public TMP_InputField nameInputField;
        public Button restartBtn;
        public Button rankingBtn;
        public Button menuBtn;

        public RectTransform resultEntriesRoot;
        public GameObject resultEntryPrefab;

        public void Start()
        {
            restartBtn.onClick.AddListener(GameManager.Instance.LoadPlayScene);
            rankingBtn.onClick.AddListener(GameManager.Instance.ShowRanking);
            menuBtn.onClick.AddListener(GameManager.Instance.LoadMenuScene);
            nameInputField.onEndEdit.AddListener(PlaySceneController.Instance.SaveScore);
        }

        public void InitWithPlayDataResult(PlayData data)
        {
            resultEntriesRoot.DestroyChildren();
            //spawn resultEntry
            var allFoodNames = data.EatenFood.Keys.ToList();
            foreach (var foodName in allFoodNames)
            {
                var resultEntry = Instantiate(resultEntryPrefab, resultEntriesRoot).GetComponent<ResultEntryUI>();
                resultEntry.SetText(foodName.ToString(), data.EatenFood[foodName].ToString(),
                    (GameManager.GetFoodScore(foodName) * data.EatenFood[foodName]).ToString());
            }

            scoreText.text = data.Score.ToString();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            contentRectTransform.anchoredPosition = contentRectTransform.anchoredPosition.Offset(y: -1440);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}