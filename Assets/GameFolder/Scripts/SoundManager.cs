using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    static AudioSource audisrc;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(instance);
    }
    public void Start()
    {
        audisrc = GetComponent<AudioSource>();
        SetSound();
    }

    void SetSound()
    {
        if(PlayerPrefs.GetInt("Sound") == 0)
        {
            audisrc.mute = false;
        }
        else
        {
            audisrc.mute = true;
        }
    }
}
