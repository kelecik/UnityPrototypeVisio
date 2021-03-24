using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UIManager : MonoBehaviour
{
    public List<GameObject> heartTexture = new List<GameObject>();
    [Space]
    public GameObject rewardButton, laterButton, playButton;
    public TMP_Text collectObject;
    public Slider progressBar;
    public GameObject inGameUI;
    public float sliderValue;
    [Space]
    public Animator soundAnimator;
    public GameObject settingImage;
    public GameObject soundImage;
    public GameObject pauseImage;
    bool settingIsOpen = false;
    bool pauseIsOpen = false;
    [Space]
    GameData gameData;
    GameManager gameManager;
    AudioSource soundManager;
    [Space]
    public UnityEvent gameStart;
    public UnityEvent saveHeartCount;

    private void Awake()
    {
        #region Class
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        #endregion
        #region Components
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>(); 
        #endregion
    }
    private void Update()
    {
        UIImageControl();
        HeartTextureControl();
        SliderControl();
    }

    public void PauseSetter()
    {
        if (!pauseIsOpen)
        {
            gameManager.gameState = GameState.Pause;
            pauseIsOpen = true;
        }
        else if (pauseIsOpen)
        {
            gameManager.gameState = GameState.Playing;
            pauseIsOpen = false;
        }       
    }
    public void ShowSoundButton()
    {
        if (!settingIsOpen)
        {
            soundAnimator.SetBool("SoundAnimOpen",true);
            soundAnimator.SetBool("SoundAnimClose", false);
            settingIsOpen = true;
        }
        else if (settingIsOpen)
        {
            soundAnimator.SetBool("SoundAnimOpen", false);
            soundAnimator.SetBool("SoundAnimClose", true);
            settingIsOpen = false;
        }
    }

    public void SoundSetter()
    {
        if (PlayerPrefs.GetInt("Sound") ==0)
        {
            soundManager.mute = true;
            PlayerPrefs.SetInt("Sound", 1);
        }
        else if (PlayerPrefs.GetInt("Sound") == 1)
        {
            soundManager.mute = false;
            PlayerPrefs.SetInt("Sound", 0);
        }     
    }

    public void PlayButton()
    {
        gameData.heartCount -= 1;
        gameStart.Invoke();
        saveHeartCount.Invoke();
    }

    public void LaterButton()
    {
        Debug.Log("Geriye 23:47 kaldı");
    }

    public void RewardButton()
    {
        gameData.heartCount = 3;
        gameManager.gameState = GameState.Playable;
        Debug.Log("Video İzlendi");
    }

    public void CollectObject()
    {
        gameData.collectableObjects += 0.05f;
        collectObject.text = "Coin : " + (Mathf.Round(gameData.collectableObjects * 20)).ToString();
    }

    void UIImageControl()
    {
        switch (gameManager.gameState)
        {
            case GameState.Playable:
                playButton.SetActive(true);
                rewardButton.SetActive(false);
                laterButton.SetActive(false);
                inGameUI.SetActive(false);
                settingImage.SetActive(true);
                pauseImage.SetActive(false);
                soundImage.SetActive(true);
                break;
            case GameState.Playing:
                playButton.SetActive(false);
                rewardButton.SetActive(false);
                laterButton.SetActive(false);
                inGameUI.SetActive(true);
                settingImage.SetActive(false);
                pauseImage.SetActive(false);
                pauseImage.SetActive(true);
                soundImage.SetActive(false);
                break;
            case GameState.NotPlayable:
                playButton.SetActive(false);
                rewardButton.SetActive(true);
                laterButton.SetActive(true);
                inGameUI.SetActive(false);
                settingImage.SetActive(false);
                pauseImage.SetActive(false);
                soundImage.SetActive(false);
                break;
            case GameState.Pause:
                pauseImage.SetActive(true);
                break;
        }
    } 
    void HeartTextureControl()
    {
        for (int i = 0; i < heartTexture.Count; i++)
        {
            if (i < gameData.heartCount)
            {
                heartTexture[i].SetActive(true);
            }
            else
            {
                heartTexture[i].SetActive(false);
            }
        }
    }
    void SliderControl()
    {
        progressBar.value = gameData.collectableObjects;
    }
}
