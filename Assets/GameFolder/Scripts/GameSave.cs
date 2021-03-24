using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    GameData gameData;

    private void Awake()
    {
        #region Class
        gameData = GetComponent<GameData>();
        #endregion
        FirstTimeSave();
    }

    public void HeartCountSave()
    {
        PlayerPrefs.SetInt("HeartCount", gameData.heartCount);
    }

    public void FirstTimeSave()
    {
        if (PlayerPrefs.GetInt("FirstTimeSave") == 0)
        {
            PlayerPrefs.SetInt("FirstTimeSave", 1);
            PlayerPrefs.SetInt("HeartCount", 3);
        }
    }
}
