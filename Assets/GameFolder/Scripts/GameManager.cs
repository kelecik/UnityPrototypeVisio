using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState gameState = GameState.Playable;
    GameData gameData;
    private int noneHeartCount = 0; // belirteç

    private void Awake()
    {
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }
    public void GameStart()
    {
        StartCoroutine(GameStartWithDelay(0.1f));
    }

    public void GameFinish()
    {
        StartCoroutine(GameFinishWithDelay(0.5f));
    }

    IEnumerator GameStartWithDelay(float delay)
    {
        if (gameData.heartCount < noneHeartCount)
        {
            gameState = GameState.NotPlayable;
        }
        else
        {
            yield return new WaitForSeconds(delay);
            gameState = GameState.Playing;
        }
    }

    IEnumerator GameFinishWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameState = GameState.Playable;
        SceneManager.LoadScene(1);
    }
}
