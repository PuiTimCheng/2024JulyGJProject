using UnityEngine;
using UnityEngine.Serialization;

namespace UI.GameCanvasUIManager
{
    public class GameCanvasUIManager : TimToolBox.Extensions.Singleton<GameCanvasUIManager>
    {
        public InstructionUI instructionUI;
        public GamePlayUI gamePlayUI;
        public ConclusionUI conclusionUI;
        public PlayTimerUI playTimerUI;
        public PlayScoreUI playScoreUI;
        public UIEffectManager uIEffectManager;
        public IconAnimation iconAnimation;
        public Tea tea;
        public Trash trash;
        public StartAndEndText StartAndEndText;

        public Sprite prawn2;
        public Sprite phone2;

        public void ShowInstruction()
        {
            instructionUI.Show();
            gamePlayUI.Hide();
            conclusionUI.Hide();
        }

        public void ShowGamePlayUI()
        {
            gamePlayUI.Show();
            instructionUI.Hide();
            conclusionUI.Hide();
        }
    }
}