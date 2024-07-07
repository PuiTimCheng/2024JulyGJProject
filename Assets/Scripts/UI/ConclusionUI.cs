using System;
using System.Collections.Generic;
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
        public TMP_InputField nameInputField;
        public Button restartBtn;
        public Button rankingBtn;
        public Button menuBtn;
        
        public Image redLineImage;
        public RectTransform YellowRectTransform;
        
        public RectTransform resultEntriesRoot;
        public GameObject resultEntryPrefab;


        public RectTransform[] RebuildRectTransforms;
        public void Start()
        {
            restartBtn.onClick.AddListener(GameManager.Instance.LoadPlayScene);
            rankingBtn.onClick.AddListener(GameManager.Instance.ShowRanking);
            menuBtn.onClick.AddListener(GameManager.Instance.LoadMenuScene);
            nameInputField.onEndEdit.AddListener(HandleSaveScore);

            RebuildRectTransforms = contentRectTransform.transform.GetComponentsInChildren<RectTransform>();
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
            redLineImage.gameObject.SetActive(false);
            YellowRectTransform.gameObject.SetActive(false);
            mergedTimesText.text = "";
            totalScoreText.text = "";
            
            nameInputField.interactable = true;
            
            // Create a DOTween sequence
            Sequence sequence = DOTween.Sequence();
            // Step 1: Show result entries one by one
            var allFoodNames = data.EatenFood.Keys.ToList();
            foreach (var foodName in allFoodNames)
            {
                var resultEntry = Instantiate(resultEntryPrefab, resultEntriesRoot).GetComponent<ResultEntryUI>();
                resultEntry.SetText(foodName.ToString(), data.EatenFood[foodName].ToString(),
                    (GameManager.GetFoodScore(foodName) * data.EatenFood[foodName]).ToString());

                resultEntry.transform.localScale = Vector3.zero; // Start with scale zero
                sequence.AppendCallback(() => { RebuildLayout(); });
                //sequence.AppendCallback(() => { AddHeight(100); });
                sequence.Append(resultEntry.transform.DOScale(Vector3.one, 0.3f)); // Scale to one over 0.3 seconds
                sequence.AppendInterval(0.3f);
            }

            // Step 2: Show script end
            sequence.AppendCallback(() => scriptEndRectTrans.gameObject.SetActive(true));
            //sequence.AppendCallback(() => { AddHeight(500); });
            sequence.AppendCallback(() => { RebuildLayout(); });
            //sequence.Append(scriptEndRectTrans.DOScale(Vector3.one, 0.5f)); // Scale up the script end
            sequence.AppendInterval(0.5f);
            
            // Step 3: Show merged count
            sequence.AppendCallback(() => mergedTimesText.text = data.mergedFoodCount.ToString());
            sequence.Append(mergedTimesText.DOFade(1, 0.3f)); // Fade in the merged count text
            sequence.AppendInterval(0.5f);
            // Step 4: Show total score
            sequence.AppendCallback(() => totalScoreText.text = data.Score.ToString());
            sequence.Append(totalScoreText.DOFade(1, 0.3f)); // Fade in the total score text
            sequence.AppendInterval(1f);
            // Step 4: Show effect 
            //sequence.AppendCallback(() => spawnEffectRectTrans.gameObject.SetActive(true));
            //sequence.Append(spawnEffectRectTrans.DOScale(1, 0.3f)); // Fade in the total score text
            // Start the sequence
            sequence.Play();
        }

        public void HandleSaveScore(string name)
        {
            Debug.Log($"HandleSaveScore {name}");
            nameInputField.interactable = false;
            //Show effect 
            redLineImage.fillAmount = 0;
            YellowRectTransform.localScale = Vector3.zero;
            Sequence sequence = DOTween.Sequence();
            
            sequence.AppendCallback(() => redLineImage.gameObject.SetActive(true));
            sequence.Append(redLineImage.DOFillAmount(1, 0.2f)); // Scale up the script end
            sequence.AppendInterval(0.3f);
            
            sequence.AppendCallback(() => YellowRectTransform.gameObject.SetActive(true));
            sequence.Append(YellowRectTransform.DOScale(1, 0.5f)); 
            sequence.Append(YellowRectTransform.DOPunchScale(Vector3.one * 0.1f, 0.3f));
            sequence.Play();
            
            PlaySceneController.Instance.SaveScore(name);
        }
        
        public void AddHeight(float height)
        {
            contentRectTransform.SetSize(new Vector2(contentRectTransform.sizeDelta.x , contentRectTransform.sizeDelta.y + height));
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);
        }
        public void RebuildLayout()
        {
            var count = RebuildRectTransforms.Length;
            for (int i = count-1; i >= 0; i--)
            {
                if(RebuildRectTransforms[i]) LayoutRebuilder.ForceRebuildLayoutImmediate(RebuildRectTransforms[i]);
            }
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