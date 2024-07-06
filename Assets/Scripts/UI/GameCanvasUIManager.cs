using UnityEngine;
using UnityEngine.Serialization;

namespace UI.GameCanvasUIManager
{
    public class GameCanvasUIManager : TimToolBox.Extensions.Singleton<GameCanvasUIManager>
    {
        public InstructionUI instructionUI;
        public GamePlayUI gamePlayUI;
        public ConclusionUI conclusionUI;

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