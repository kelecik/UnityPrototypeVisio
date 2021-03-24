using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    Vector3 startPos;
    Vector3 currentPos;
    Vector3 direction;
    float endXPos = 0;
    [SerializeField]
    float speed;
    [SerializeField]
    float horizontalForce;
    [SerializeField]
    float verticalForce;
    GameManager gameManager;
    PlayerDetermination playerDetermination;
    float swipeThres = 0.1f;
    Rigidbody rigidBody;

    private void Awake()
    {
        #region Class
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerDetermination = GetComponent<PlayerDetermination>();
        #endregion
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveInput();
    }

    void MoveInput()
    {
        if (gameManager.gameState == GameState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                currentPos = Input.mousePosition;
                direction = (startPos - currentPos).normalized;
                MoveDirection();
            }
            transform.Translate(Vector3.forward * speed);
        }
    }
    void MoveDirection()
    {
        float x = direction.x;
        float y = direction.y;
        if((startPos-currentPos).magnitude > swipeThres * Screen.width)
        {
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    endXPos = transform.position.x + 3;
                }
                else
                {
                    endXPos = transform.position.x - 3;
                }
            }
            else
            {
                if (y < 0 && playerDetermination.isGround)
                {
                    rigidBody.velocity =(new Vector3(0, verticalForce, horizontalForce) * 20);
                }
            }
        }       
        endXPos = Mathf.Clamp(endXPos, -3f, 3f);
        transform.DOMoveX(endXPos, .15f);
    }
}