using System;
using ImprovedTimers;
using TimToolBox.DesignPattern.StateMachine;
using UnityEngine;
using IState = TimToolBox.DesignPattern.StateMachine.IState;
using StateMachine = TimToolBox.DesignPattern.StateMachine.StateMachine;

/// <summary>
/// Contains all necessary information of one game
/// </summary>
public class GameSceneController : MonoBehaviour
{
    public static GameSceneController _singleton;

    public static GameSceneController Singleton
    {
        get
        {
            if (_singleton == null)
                _singleton = new GameObject("GameSceneController").AddComponent<GameSceneController>();
            return _singleton;
        }
    }

    public StateMachine GameStateMachine;
    public GameData GameData;

    public ConclusionUI ConclusionUI;
    
    private void Awake()
    {
        _singleton = this;
    }

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
            Singleton.ConclusionUI.Hide();
            
            Singleton.GameData = new GameData(); //reset all game data
            _playtimer.Reset(5);
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
            Singleton.ConclusionUI.Show();
        }

        public void OnUpdateState()
        {
        }

        public void OnExitState()
        {
        }
    }
}
