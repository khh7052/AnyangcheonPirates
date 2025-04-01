using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Text.RegularExpressions;
using System;

public enum GameState
{
    IDLE,
    PLAY,
    CLEAR,
    OVER
}

public class GameManager : Singleton<GameManager>
{
    public UnityEvent OnPlay = new();
    public UnityEvent OnOver = new();
    public UnityEvent OnClear = new();

    public static string PreviousSceneName;
    private static string sceneName;
    private static string[] sceneNameSplit;


    public GameState gameState;
    private float magicCount = 4;
    private int emenyCount = 0;
    private int enemyDeathCount = 0;

    protected override void OnEnable()
    {
        base.OnEnable();

        SceneNameUpdate();

        if (sceneNameSplit[0] == "Stage")
        {
            Play();
        }
        else
        {
            gameState = GameState.IDLE;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        SceneNameUpdate();

        if (sceneNameSplit[0] == "Stage")
        {
            Play();
        }
        else
        {
            gameState = GameState.IDLE;
        }
    }

    void SceneNameUpdate()
    {
        sceneName = SceneManager.GetActiveScene().name;
        sceneNameSplit = StringUtility.SplitString(sceneName);
    }

    public void MagicCountDown(float num)
    {
        if (sceneNameSplit[0] != "Stage") return;

        magicCount -= num;

        if (magicCount <= 0)
        {
            GameOver();
        }
    }


    public void Play()
    {
        Time.timeScale = 1;
        gameState = GameState.PLAY;
        print("Play!");
        MagicCount mc = FindObjectOfType<MagicCount>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        SimpleMonster[] monsters = new SimpleMonster[enemies.Length];

        magicCount = mc.initialMagicCount;
        emenyCount = enemies.Length;
        enemyDeathCount = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            monsters[i] = enemies[i].GetComponent<SimpleMonster>();
            monsters[i].OnDie.AddListener(EnemyDeath);
        }

        OnPlay.Invoke();
    }

    public void EnemyDeath()
    {
        if (sceneNameSplit[0] != "Stage") return;

        enemyDeathCount++;

        if(enemyDeathCount >= emenyCount)
        {
            GameClear();
        }
    }

    [ContextMenu("GameClear")]
    public void GameClear()
    {
        if (gameState != GameState.PLAY) return;
        gameState = GameState.CLEAR;
        print("GameClear!");

        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlaySFX("Clear");
        }

        OnClear.Invoke();
    }

    [ContextMenu("GameOver")]
    public void GameOver()
    {
        if (gameState != GameState.PLAY) return;
        gameState = GameState.OVER;
        print("GameOver!");

        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlaySFX("Over");
        }

        OnOver.Invoke();
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void NextStage()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string[] ss = StringUtility.SplitString(sceneName);


        int stage = int.Parse(ss[1]);
        stage++;
        string nextSceneName = "Stage" + stage;

        if (stage <= 10)
        {
            LoadScene("Stage" + stage);
        }
        else
        {
            LoadScene("Lobby");
        }
    }

    public void RePlay()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        PreviousSceneName = SceneManager.GetActiveScene().name;
        print(PreviousSceneName + " PreviousSceneName!");
        SceneManager.LoadScene(sceneName);
    }
}
