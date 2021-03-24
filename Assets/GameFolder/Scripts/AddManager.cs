using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AddManager : MonoBehaviour, IUnityAdsListener
{
    public string gameID = "3820071";
    public string placementID = "rewardedVideo";
    public string bannerID = "banner";
    bool again;

    GameManager gameManager;
    GameData gameData;
    //-------------------------------------------
    void Start()
    {
        #region Class
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>(); 
        #endregion
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, false);
        Invoke("banner", 1f);
        again = false;
    }
    public void GetAds()
    {
        StartCoroutine(Ads());
    }

    IEnumerator Ads()
    {
        Advertisement.Show(placementID);
        yield return new WaitForSeconds(3);
        if (again == false)
        {
            Advertisement.Show(placementID);
        }
    }
    void banner()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerID);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Failed)
        {
            Debug.Log("There was an error showing an ad");
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Could not award the reward for closing the ad");
        }
        else if (showResult == ShowResult.Finished)
        {
            gameData.heartCount = 3;
            gameManager.gameState = GameState.Playable;
            Debug.Log("video watched");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (Advertisement.IsReady(placementId))
            Debug.Log("Ads is ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("ad failed to start: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("ad failed to start : " + placementId);
        again = true;
    }
}

