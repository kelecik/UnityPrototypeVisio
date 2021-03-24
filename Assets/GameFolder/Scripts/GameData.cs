using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public int heartCount;
    public float collectableObjects;

    private void Start()
    {
        heartCount = PlayerPrefs.GetInt("HeartCount");
    }
}
