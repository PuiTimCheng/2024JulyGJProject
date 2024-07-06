using System;
using ImprovedTimers;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using TimToolBox.DesignPattern.StateMachine;
using TimToolBox.Extensions;
using UI.GameCanvasUIManager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Util;
using IState = TimToolBox.DesignPattern.StateMachine.IState;
using Random = UnityEngine.Random;
using StateMachine = TimToolBox.DesignPattern.StateMachine.StateMachine;

/// <summary>
/// Contains all necessary information of one game
/// </summary>
public class PlaySceneController : TimToolBox.Extensions.Singleton<PlaySceneController>
{
    public StateMachine GameStateMachine;
    public FoodConveyor foodConveyor;
    public PlayData PlayData;
    
    private void Start()
    {
        GameStateMachine = new StateMachine();

        var instructionState = new InstructionState();
        var playState = new PlayState();
        var conclusionState = new ConclusionState();
        
        GameStateMachine.AddTransition(instructionState, playState, new FuncPredicate(instructionState.IsClicked));
        GameStateMachine.AddTransition(playState, conclusionState, new FuncPredicate(playState.IsTimesUp));
        
        GameStateMachine.ChangeStateTo<InstructionState>();
    }

    private void Update()
    {
        GameStateMachine.Update();
        
        //TODO test score
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            AddScore(1);
        }
    }
    
    public void AddScore(int score)
    {
        PlayData.Score += score;
        GameCanvasUIManager.Instance.playScoreUI.UpdateScore(PlayData.Score);
    }

    public void PauseAndUnPause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
    
    [Button]
    public void EndGameNow()
    {
        GameStateMachine.ChangeStateTo<ConclusionState>();
    }

    public void SaveScore(string playerName)
    {
        var dict = GameManager.Instance.LoadScore();
        dict.Add(playerName, PlayData.Score);
        GameManager.Instance.SaveScore(dict);
        Debug.Log($"Score Saved {playerName}:{PlayData.Score}");
    }
    
    public class InstructionState : IState
    {
        private bool _clicked;
        public void OnEnterState()
        {
            _clicked = false;
            GameCanvasUIManager.Instance.ShowInstruction();
        }

        public void OnUpdateState()
        {
            if(Input.GetMouseButtonDown(0)) _clicked = true;
        }

        public void OnExitState()
        {
        }

        public bool IsClicked()
        {
            return _clicked;
        }
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
            GameCanvasUIManager.Instance.ShowGamePlayUI();
            Instance.PlayData = new PlayData(); //reset all game data
            _playtimer.Reset(90);
            _playtimer.Start();
            
            Instance.foodConveyor.Initiate(new System.Random().Next());
            GameCanvasUIManager.Instance.playScoreUI.UpdateScore(Instance.PlayData.Score);
        }

        public void OnUpdateState()
        {
            GameCanvasUIManager.Instance.playTimerUI.UpdateTimeRatio(_playtimer.Progress);
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
            GameCanvasUIManager.Instance.conclusionUI.Show();
            GameCanvasUIManager.Instance.conclusionUI.scoreText.text = Instance.PlayData.Score.ToString();
        }

        public void OnUpdateState()
        {
        }

        public void OnExitState()
        {
        }
    }
}
