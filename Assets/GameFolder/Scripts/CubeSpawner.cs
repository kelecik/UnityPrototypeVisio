using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public Transform target;
    public Transform container;
    float time;
    [SerializeField] float spawnFrequency;
    [SerializeField] int objectCount;
    [SerializeField] GameObject cube;
    GameManager gameManager;


    private void Update()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Spawner();
    }

    void Spawner()
    {
        if(gameManager.gameState == GameState.Playing)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                float x = Random.Range(0, 2);
                float z = Random.Range(target.position.z + 10, target.position.z + 50);
                for (int i = 0; i < objectCount; i++)
                {
                    Instantiate(cube, new Vector3(x * 3, 1, z +(i*3)), Quaternion.identity, container);
                    time = spawnFrequency;
                }
            }
        }  
    }
}
