using System;
using System.Linq;
using DG.Tweening;
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
        public TextMeshProUGUI mergedTimesText;
        public TextMeshProUGUI totalScoreText;
        public RectTransform scriptEndRectTrans;
        public ScrollRect ScrollRect;
        public RectTransform spawnEffectRectTrans;
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

        private void Update()
        {
            var curPos = contentRectTransform.anchoredPosition;
            var newPos = Mathf.Lerp(ScrollRect.normalizedPosition.y, 0, 0.6f);
            ScrollRect.normalizedPosition = ScrollRect.normalizedPosition.Set(y: 0);
            /*if(contentRectTransform.anchoredPosition.y >= 0.1f)
                contentRectTransform.anchoredPosition = contentRectTransform.anchoredPosition.Set(y: newPos);*/
        }

        public void ShowWithPlayDataResult(PlayData data)
        {
            resultEntriesRoot.DestroyChildren();
            scriptEndRectTrans.gameObject.SetActive(false);
            spawnEffectRectTrans.gameObject.SetActive(false);
            mergedTimesText.text = "";
            totalScoreText.text = "";
            
            AudioManager.Instance.PlaySFX(SFXType.R_Start);
            
            // Create a DOTween sequence
            Sequence sequence = DOTween.Sequence();
            // Step 1: Show result entries one by one
            var allFoodNames = data.EatenFood.Keys.ToList();
            sequence.AppendInterval(1f);

            foreach (var foodName in allFoodNames)
            {
                var resultEntry = Instantiate(resultEntryPrefab, resultEntriesRoot).GetComponent<ResultEntryUI>();
                resultEntry.SetText(foodName.ToString(), data.EatenFood[foodName].ToString(),
                    (GameManager.GetFoodScore(foodName) * data.EatenFood[foodName]).ToString());

                resultEntry.transform.localScale = Vector3.zero; // Start with scale zero
                sequence.AppendCallback(() => AudioManager.Instance.PlaySFX(SFXType.R_Print));
                sequence.Append(resultEntry.transform.DOScale(Vector3.one, 0.3f)); // Scale to one over 0.3 seconds
                sequence.AppendCallback(()=>{ LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform); });
                sequence.AppendCallback(()=>{ LayoutRebuilder.ForceRebuildLayoutImmediate(resultEntriesRoot); });
                //sequence.AppendCallback(()=>{ ScrollRect.normalizedPosition = ScrollRect.normalizedPosition.Set(y: 0); });
                sequence.AppendInterval(0.3f);
            }

            // Step 2: Show script end
            sequence.AppendCallback(() => scriptEndRectTrans.gameObject.SetActive(true));
            sequence.Append(scriptEndRectTrans.DOScale(Vector3.one, 0.5f)); // Scale up the script end
            sequence.AppendCallback(()=>{ LayoutRebuilder.ForceRebuildLayoutImmediate(scriptEndRectTrans); });
            sequence.AppendCallback(()=>{ LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform); });
            sequence.AppendCallback(()=>{ ScrollRect.normalizedPosition = ScrollRect.normalizedPosition.Set(y: 0); });
            sequence.AppendCallback(() => { contentRectTransform.anchoredPosition = Vector2.zero; });
            
            sequence.AppendInterval(0.5f);
            
            // Step 3: Show merged count
            sequence.AppendCallback(() => AudioManager.Instance.PlaySFX(SFXType.R_Print));
            sequence.AppendCallback(() => mergedTimesText.text = data.mergedFoodCount.ToString());
            sequence.Append(mergedTimesText.DOFade(1, 0.3f)); // Fade in the merged count text
            sequence.AppendInterval(0.5f);
            // Step 4: Show total score
            sequence.AppendCallback(() => AudioManager.Instance.PlaySFX(SFXType.R_Print));
            sequence.AppendCallback(() => totalScoreText.text = data.Score.ToString());
            sequence.Append(totalScoreText.DOFade(1, 0.3f)); // Fade in the total score text
            sequence.AppendInterval(1f);
            // Step 4: Show effect 
            sequence.AppendCallback(() => 
                AudioManager.Instance.PlaySFX(SFXType.R_End));
            sequence.AppendCallback(() => 
                spawnEffectRectTrans.gameObject.SetActive(true));
            sequence.Append(spawnEffectRectTrans.DOScale(1, 0.3f)); // Fade in the total score text
            // Start the sequence
            sequence.Play();
            
            
            /*//TODO dotween sequence
            // show result entry one by one
            // show script end
            scriptEndRectTrans.gameObject.SetActive(true);
            // show merged count
            mergedTimesText.text = data.mergedFoodCount.ToString();
            // show totalScore
            totalScoreText.text = data.Score.ToString();
            
            //spawn resultEntry
            var allFoodNames = data.EatenFood.Keys.ToList();
            foreach (var foodName in allFoodNames)
            {
                var resultEntry = Instantiate(resultEntryPrefab, resultEntriesRoot).GetComponent<ResultEntryUI>();
                resultEntry.SetText(foodName.ToString(), data.EatenFood[foodName].ToString(),
                    (GameManager.GetFoodScore(foodName) * data.EatenFood[foodName]).ToString());
            }*/

            
        }

        public void Show()
        {
            gameObject.SetActive(true);
            contentRectTransform.anchoredPosition = contentRectTransform.anchoredPosition.Set(y: -500);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}