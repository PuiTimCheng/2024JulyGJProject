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
            startBtn.onClick.AddListener(OpenPlayScene);
            rankingBtn.onClick.AddListener(GameManager.Instance.ShowRanking);
            creditsBtn.onClick.AddListener(ShowCredits);
            AudioManager.Instance.PlayBGM(BGMType.Menu);
        }

        void OpenPlayScene()
        {
            AudioManager.Instance.PlaySFX(SFXType.Start);
            GameManager.Instance.LoadPlayScene();
        }

        private void ShowCredits()
        {
            //show credits ui here
            Debug.Log("credits");
        }
    }
}
