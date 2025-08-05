using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Ready,
    Play,
    Play_inactive,
    Goal,
    Clear,
    Fail
}
public class GameManager : MonoBehaviour
{
    public static GameManager i;

    public float confettiPlayDelayTime_sec = 0.5f;
    public float openClearCanvasDelayTime_sec = 1.5f;
    public float openFailCanvasDelayTime_sec = 1f;
    private int currentLevel;

    [HideInInspector] public GameState gameState = GameState.Ready;

    //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
    void Awake()
    {
        i = this;
    }

    void Update()
    {
        CheckFail();
    }

    //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
    public void BeginGame()
    {
        if (gameState != GameState.Ready) return;

        gameState = GameState.Play;
        PlayerManager.i.BeginPlayersRunning();
    }

    public void BeginGoal()
    {
        if (gameState != GameState.Play) return;

        gameState = GameState.Goal;
        SoundManager.Instance.StopBGSound();
        SoundManager.Instance.PlaySound(SoundType.Victory);
        CameraController.i.MoveGoalPosition();
        StageController.i.PlaySparks();
        StageController.i.FadeoutGoalFlag();
    }

    public void BeginClear()
    {
        if (gameState != GameState.Goal) return;

        gameState = GameState.Clear;
        PlayerManager.i.StopPlayersRunning_Clear();
        StartCoroutine(DelayMethod(confettiPlayDelayTime_sec, () =>
        {
            ConfettiManager.i.PlayConfetti();
        }));
        StartCoroutine(DelayMethod(openClearCanvasDelayTime_sec, () =>
        {
            currentLevel = PlayerPrefs.GetInt("level", 0) + 1;
            CanvasManager.i.OpenClearCanvas(currentLevel);
            if (currentLevel >= 5)
            {
                int player = PlayerPrefs.GetInt("playerIndex", 0);
                player++;
                if (player >= 5)
                {
                    player = 0;
                }
                PlayerPrefs.SetInt("playerIndex", player);
                currentLevel = 0;
            }
            PlayerPrefs.SetInt("level", currentLevel);
        }));
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("level", 0);
    }

    public void CheckFail()
    {
        if (PlayerManager.i.players.Count > 0) return;

        gameState = GameState.Fail;
        StartCoroutine(DelayMethod(openFailCanvasDelayTime_sec, () =>
        {
            SoundManager.Instance.PlaySound(SoundType.Lose);
            SoundManager.Instance.StopBGSound();
            CanvasManager.i.OpenFailCanvas();
        }));
    }

    //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
    IEnumerator DelayMethod(float delayTime_sec, Action action) { yield return new WaitForSeconds(delayTime_sec); action(); }
}
