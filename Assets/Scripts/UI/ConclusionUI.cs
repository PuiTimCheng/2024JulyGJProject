using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameCanvasUIManager
{
    public class ConclusionUI : MonoBehaviour, IUIPanel
    {
        public TextMeshProUGUI scoreText;
        public TMP_InputField nameInputField;
        public Button restartBtn;
        public Button rankingBtn;
        public Button menuBtn;

        public void Start()
        {
            restartBtn.onClick.AddListener(GameManager.Instance.LoadPlayScene);
            rankingBtn.onClick.AddListener(GameManager.Instance.ShowRanking);
            menuBtn.onClick.AddListener(GameManager.Instance.LoadMenuScene);
            nameInputField.onEndEdit.AddListener(PlaySceneController.Instance.SaveScore);
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
