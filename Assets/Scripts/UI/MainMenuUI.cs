using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public Button rankingBtn;
        public Button creditsBtn;

        private void Start()
        {
            rankingBtn.onClick.AddListener(ShowRanking);
            creditsBtn.onClick.AddListener(ShowCredits);
        }

        private void ShowRanking()
        {
            //show ranking ui here
            Debug.Log("ranking");
        }

        private void ShowCredits()
        {
            //show credits ui here
            Debug.Log("credits");
        
        }
    }
}
