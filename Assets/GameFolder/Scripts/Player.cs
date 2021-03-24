using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private Vector2 startPos, endPos, difference;
    private float swipeThreshold = 0.15f;
    private float swipeTimeLimit = 0.25f;
    private float startTime, endTime;
    private float endXPos = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = endPos = Input.mousePosition;
            startTime = endTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            endTime = Time.time;

            if (endTime - startTime <= swipeTimeLimit)
            {
                DetectSwipe();
            }
        }
    }

    void DetectSwipe()
    {
        difference = endPos - startPos;

        if (difference.magnitude > swipeThreshold * Screen.width)
        {
            if (difference.x > 0)
            {
                endXPos = transform.position.x + 3f;
            }
            else if (difference.x < 0)
            {
                endXPos = transform.position.x - 3f;
            }
            else if(difference.y > 0)
            {
                transform.DOMoveY(transform.position.y + 3, 0.2f);
                Debug.Log("giriyor");
            }
            Debug.Log(difference.magnitude);
        }
        endXPos = Mathf.Clamp(endXPos, -3f, 3f);
        transform.DOMoveX(endXPos, .15f);
    }
}
