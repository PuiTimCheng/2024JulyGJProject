using System;
using ImprovedTimers;
using MoreMountains.Feedbacks;
using TimToolBox.DesignPattern.StateMachine;
using TimToolBox.Extensions;
using UI.GameCanvasUIManager;
using UnityEngine;
using UnityEngine.Serialization;
using Util;
using IState = TimToolBox.DesignPattern.StateMachine.IState;
using StateMachine = TimToolBox.DesignPattern.StateMachine.StateMachine;

/// <summary>
/// Contains all necessary information of one game
/// </summary>
public class PlaySceneController : TimToolBox.Extensions.Singleton<PlaySceneController>
{
    public StateMachine GameStateMachine;
    public GameData GameData;
    
    private void Start()
    {
        GameStateMachine = new StateMachine();

        var playState = new PlayState();
        var conclusionState = new ConclusionState();
        
        GameStateMachine.AddTransition(playState, conclusionState, new FuncPredicate(playState.IsTimesUp));
        
        GameStateMachine.ChangeStateTo<PlayState>();
    }

    private void Update()
    {
        GameStateMachine.Update();
    }

    public class PlayState : IState
    {
        private CountdownTimer _playtimer;

        public PlayState()
        {
            _playtimer = new CountdownTimer(1);
        }
        public void OnEnterState()
        {
            GameCanvasUIManager.Instance.conclusionUIPanel.Hide();
            Instance.GameData = new GameData(); //reset all game data
            _playtimer.Reset(2);
            _playtimer.Start();
        }

        public void OnUpdateState()
        {
            Debug.Log($"PlayTime : {_playtimer.CurrentTime}");
        }

        public void OnExitState()
        {
        }

        public bool IsTimesUp()
        {
            return _playtimer.IsFinished;
        }
    }
    
    public class ConclusionState : IState
    {
        public void OnEnterState()
        {
            GameCanvasUIManager.Instance.conclusionUIPanel.Show();
        }

        public void OnUpdateState()
        {
        }

        public void OnExitState()
        {
        }
    }
}
