using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public Button startBtn;
        public Button rankingBtn;
        public Button creditsBtn;

        public float punchStrength;
        public bool clicked;
        
        private void Start()
        {
            startBtn.onClick.AddListener(()=> { ClickButtonAnimation(startBtn, GameManager.Instance.LoadComicScene); });
            rankingBtn.onClick.AddListener(()=> { ClickButtonAnimation(rankingBtn, GameManager.Instance.ShowRanking); } );
            creditsBtn.onClick.AddListener(()=> { ClickButtonAnimation(creditsBtn, ShowCredits); } );
        }

        private void ClickButtonAnimation(Button btn, Action afterAcion)
        {
            if (clicked) return;
            clicked = true;
            btn.GetComponent<RectTransform>().DOPunchScale(Vector3.one * punchStrength, 0.1f).OnComplete(()=>
            {
                clicked = false;
                afterAcion.Invoke();
            });
        }
        
        private void ShowCredits()
        {
            //show credits ui here
            Debug.Log("credits");
        }
    }
}
