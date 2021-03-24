using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetermination : MonoBehaviour
{
    GameData gameData;
    public bool isGround;
    public UnityEvent finish;
    public UnityEvent CollectObject;

    private void Awake()
    {
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case 9:
                Destroy(other.gameObject);
                CollectObject.Invoke();
                break;
            case 10:
                finish.Invoke();
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 8)
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 8)
        {
            isGround = false;
        }
    }
}
