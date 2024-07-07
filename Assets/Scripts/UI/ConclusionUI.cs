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
            if (Input.GetMouseButtonDown(0))
            {
                SpeedUpSequenceOnClick();
            }
        }

        private Sequence sequence;
        private bool canSpeedUP;
        public void ShowWithPlayDataResult(PlayData data)
        {
            resultEntriesRoot.DestroyChildren();
            scriptEndRectTrans.gameObject.SetActive(false);
            redLineImage.gameObject.SetActive(false);
            YellowRectTransform.gameObject.SetActive(false);
            RebuildLayout();
            
            contentRectTransform.anchoredPosition = contentRectTransform.anchoredPosition.Set(y: -1200);
            
            mergedTimesText.text = "";
            totalScoreText.text = "";
            
            nameInputField.interactable = true;
            AudioManager.Instance.PlaySFX(SFXType.R_Start);
            
            // Create a DOTween sequence
            sequence = DOTween.Sequence();
            // Step 1: Show result entries one by one
            var allFoodNames = data.EatenFood.Keys.ToList();
            sequence.AppendInterval(1f);

            foreach (var foodName in allFoodNames)
            {
                var resultEntry = Instantiate(resultEntryPrefab, resultEntriesRoot).GetComponent<ResultEntryUI>();
                resultEntry.SetText(UIEffectManager.GetFoodLabel(foodName), data.EatenFood[foodName].ToString(),
                    (GameManager.GetFoodScore(foodName) * data.EatenFood[foodName]).ToString());
                var rectTrans = resultEntry.GetComponent<RectTransform>();
                rectTrans.sizeDelta = new Vector2(rectTrans.sizeDelta.x, 0); // Start with scale zero
                rectTrans.localScale = Vector3.zero; // Start with scale zero
                
                sequence.AppendCallback(() => { canSpeedUP = true; });
                sequence.AppendCallback(() => { RebuildLayout(); });
                sequence.AppendCallback(() => AudioManager.Instance.PlaySFX(SFXType.R_Print));
                sequence.AppendCallback(() => rectTrans.sizeDelta = new Vector2(rectTrans.sizeDelta.x, 100));
                sequence.AppendCallback(() => rectTrans.localScale = Vector3.one);
                //sequence.Append(rectTrans.DOScale(Vector3.one, 0.3f)); // Scale to one over 0.3 seconds
                sequence.AppendInterval(0.3f);
            }

            // Step 2: Show script end
            sequence.AppendCallback(() => scriptEndRectTrans.gameObject.SetActive(true));
            //sequence.AppendCallback(() => { AddHeight(500); });
            sequence.AppendCallback(() => { RebuildLayout(); });
            //sequence.Append(scriptEndRectTrans.DOScale(Vector3.one, 0.5f)); // Scale up the script end
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
            sequence.AppendCallback(() => { canSpeedUP = false; });
            sequence.AppendCallback(() => { sequence = null; });
            
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
            var sequence = DOTween.Sequence();
            
            sequence.AppendCallback(() => 
                AudioManager.Instance.PlaySFX(SFXType.R_End));
            sequence.AppendCallback(() => redLineImage.gameObject.SetActive(true));
            sequence.Append(redLineImage.DOFillAmount(1, 0.2f)); // Scale up the script end
            sequence.AppendInterval(0.3f);
            
            sequence.AppendCallback(() => YellowRectTransform.gameObject.SetActive(true));
            sequence.Append(YellowRectTransform.DOScale(1, 0.5f)); 
            sequence.Append(YellowRectTransform.DOPunchScale(Vector3.one * 0.1f, 0.3f));
            
            sequence.AppendCallback(() => GameManager.Instance.ShowRanking());
            sequence.Play();
            
            PlaySceneController.Instance.SaveScore(name);
        }
        
        public void SpeedUpSequenceOnClick()
        {
            if (sequence != null && sequence.IsPlaying() && canSpeedUP)
            {
                sequence.timeScale = 10.0f; // Double the speed of the sequence
            }
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
            //contentRectTransform.anchoredPosition = contentRectTransform.anchoredPosition.Set(y: -500);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}