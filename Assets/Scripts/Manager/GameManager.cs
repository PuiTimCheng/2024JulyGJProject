using MoreMountains.Feedbacks;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        private IGameState _currentState;

        private void Update()
        {
            _currentState?.Update();
        }

        public void ChangeState(IGameState newState)
        {
            _currentState?.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }

        public IGameState CurrentState()
        {
            return _currentState;
        }

        public void EnterMainScene()
        {
            GetComponent<MMF_Player>().PlayFeedbacks();
        }
    }
}