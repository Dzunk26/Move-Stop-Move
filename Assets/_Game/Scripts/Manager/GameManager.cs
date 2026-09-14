using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State {
    Loading,
    MainMenu,
    GamePause,
    GamePlaying,
    Lose,
    Win
}

public class GameManager : Singleton<GameManager> {
    public event EventHandler OnStateChanged;

    private State currentState;

    private void Start() {
        LoadingGame();
    }

    private void OnDestroy() {
        Cache.Reset();
    }

    private void ChangeState(State state) {
        currentState = state;
        OnStateChanged?.Invoke(this, new EventArgs());
    }

    public void LoadingGame() {
        ChangeState(State.Loading);
        DataManager.Instance.OnInit();
        PlayerProgress.Instance.OnInit();
        DataManager.Instance.LoadGame();
        OnLoadLevel();

        UIManager.Instance.OpenUI<CanvasLoading>();
        UIManager.Instance.OpenUI<CanvasJoystick>();
    }

    public void GameInMainMenu() {
        ChangeState(State.MainMenu);
    }

    public void PlayGame() {
        ChangeState(State.GamePlaying);
    }

    public void PauseGame() {
        ChangeState(State.GamePause);
    }

    public void WinGame() {
        ChangeState(State.Win);
        PlayerProgress.Instance.NextLevel();
        DataManager.Instance.SaveGame();
        UIManager.Instance.OpenUI<CanvasVictory>();
    }

    public void LoseGame() {
        ChangeState(State.Lose);
        UIManager.Instance.OpenUI<CanvasLose>();
    }

    public bool IsLoadingGame() {
        return currentState == State.Loading;
    }

    public bool IsInMainMenu() {
        return currentState == State.MainMenu;
    }

    public bool IsPlayingGame() {
        return currentState == State.GamePlaying;
    }

    public bool IsPauseGame() {
        return currentState == State.GamePause;
    }

    public bool IsWinGame() {
        return currentState == State.Win;
    }

    public bool IsLoseGame() {
        return currentState == State.Lose;
    }

    public void OnLoadLevel() {
        int levelID = PlayerProgress.Instance.GetCurrentLevelID();
        LevelManager.Instance.LoadLevel(levelID);
        int totalBotAmount = LevelManager.Instance.GetCurrentTotalBotAmount();
        int maxActiveBot = LevelManager.Instance.GetCurrentMaxActiveBot();
        Level currentLevel = LevelManager.Instance.GetCurrentLevel();
        BotManager.Instance.OnLoadLevel(totalBotAmount, maxActiveBot, currentLevel);
    }

    public void OnReloadLevel() {
        LevelManager.Instance.ResetCurrentLevel();
    }
}