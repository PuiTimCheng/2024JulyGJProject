using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameCanvasUIManager
{
    public class ConclusionUIPanel : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public Button restartBtn;
        public Button rankingBtn;
        public Button menuBtn;

        public void Start()
        {
            restartBtn.onClick.AddListener(GameManager.Instance.LoadPlayScene);
            rankingBtn.onClick.AddListener(GameManager.Instance.ShowRanking);
            menuBtn.onClick.AddListener(GameManager.Instance.ShowRanking);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        
    }
}
