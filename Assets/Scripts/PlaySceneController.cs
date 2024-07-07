using System;
using System.Collections.Generic;
using Battle;
using DG.Tweening;
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
    public void AddEatenDish(FoodName foodName)
    {
        var oldValue = PlayData.EatenFood.GetValueOrDefault(foodName, 0);
        if (oldValue == 0) PlayData.EatenFood.Add(foodName, 1);
        else PlayData.EatenFood[foodName] = oldValue + 1;
        Debug.Log($"Add dish {foodName} {oldValue} => {PlayData.EatenFood[foodName]}");
    }
    public void AddMergeCount()
    {
        PlayData.mergedFoodCount += 1;
    }
    
    public void PauseAndUnPause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
    
    [Button]
    public void FakeData()
    {
        PlayData.Score += 865;
        PlayData.mergedFoodCount += 13;
        PlayData.EatenFood = new Dictionary<FoodName, int>()
        {
            { FoodName.Biscuit , 10},
            { FoodName.Bread , 10},
            { FoodName.Cake , 10},
            { FoodName.Chicken , 10},
            { FoodName.Fish , 10},
            { FoodName.Mushroom , 10},
            { FoodName.Beef , 10},
            { FoodName.Egg , 10},
            { FoodName.ChickenAndBiscuit , 10},
            { FoodName.ShrimpAndBiscuit , 10},
            { FoodName.Broccoli , 10},
        };
    }
    [Button]
    public void EndGameNow()
    {
        GameStateMachine.ChangeStateTo<ConclusionState>();
    }

    public void SaveScore(string playerName)
    {
        var dict = GameManager.Instance.LoadScore();
        if (dict.ContainsKey(playerName))
        {
            var value = PlayData.Score > dict[playerName]? PlayData.Score : dict[playerName];
            dict[playerName] = value;
        }
        else
        {
            dict.Add(playerName, PlayData.Score);
        }
        GameManager.Instance.SaveScore(dict);
        Debug.Log($"Score Saved {playerName}:{PlayData.Score}");
    }
    
    public class InstructionState : IState
    {
        private bool _clicked;
        public void OnEnterState()
        {
            _clicked = false;
            AudioManager.Instance.PauseBGM();
            AudioManager.Instance.PlaySFX(SFXType.GameStart);
            AudioManager.Instance.FadeInAmbience();
            GameCanvasUIManager.Instance.ShowInstruction();
        }

        public void OnUpdateState()
        {
            if(Input.GetMouseButtonDown(0)) {_clicked = true;}
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
        private CountdownTimer _trashTalkTimer;
        
        private bool startedGame;
        
        public PlayState()
        {
            _playtimer = new CountdownTimer(1);
            _trashTalkTimer = new CountdownTimer(7);
        }
        public void OnEnterState()
        {
            startedGame = false;
            _trashTalkTimer.Start();
            
            GameCanvasUIManager.Instance.ShowGamePlayUI();
            Instance.PlayData = new PlayData(); //reset all game data
            
            AudioManager.Instance.PlayBGM(BGMType.Game);
            GameCanvasUIManager.Instance.playScoreUI.UpdateScore(Instance.PlayData.Score);
            GameCanvasUIManager.Instance.StartAndEndText.ShowText("时间不多了哦，抓紧时间吃！");
            
            DOVirtual.DelayedCall(2, () =>
            {
                //after 2 second delay,
                _playtimer.Reset(90);
                _playtimer.Start();
                startedGame = true;
                
                Instance.foodConveyor.Initiate(new System.Random().Next());
            });
        }

        public void OnUpdateState()
        {
            if (_trashTalkTimer.IsFinished)
            {
                GameCanvasUIManager.Instance.talkUI.StartDialogue();
                _trashTalkTimer.Start();
            }
            GameCanvasUIManager.Instance.playTimerUI.UpdateTimeRatio(_playtimer.Progress);
        }

        public void OnExitState()
        {
            AudioManager.Instance.PauseBGM();
        }

        public bool IsTimesUp()
        {
            return startedGame && _playtimer.IsFinished;
        }
    }
    
    public class ConclusionState : IState
    {
        private bool showedResult;
        public void OnEnterState()
        {
            showedResult = false;
            //stop everything
            FoodManager.Instance.ClearFood();
            instance.foodConveyor.Stop();
            GameCanvasUIManager.Instance.StartAndEndText.ShowText("时间到了！没把你撑着吧？");
            
            DOVirtual.DelayedCall(2, () =>
            {
                GameCanvasUIManager.Instance.playTimerUI.StopFire();
                GameCanvasUIManager.Instance.conclusionUI.Show();
                GameCanvasUIManager.Instance.conclusionUI.ShowWithPlayDataResult(Instance.PlayData);
            });
        }

        public void OnUpdateState()
        {
        }

        public void OnExitState()
        {
            AudioManager.Instance.FadeOutAmbience();
        }
    }
}
