using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public Button startBtn;
        public Button rankingBtn;
        public Button creditsBtn;

        private void Start()
        {
            startBtn.onClick.AddListener(GameManager.Instance.LoadPlayScene);
            rankingBtn.onClick.AddListener(GameManager.Instance.ShowRanking);
            creditsBtn.onClick.AddListener(ShowCredits);
        }

        private void ShowCredits()
        {
            //show credits ui here
            Debug.Log("credits");
        }
    }
}
