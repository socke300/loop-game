using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    public string[] animationDirections = { "N", "NW", "W", "SW", "S", "SE", "E", "NE" };

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDirection(Vector2 fromDirection)
    {
        int dindex = directionIndex(fromDirection);
        string dir = animationDirections[dindex];
        animator.Play(dir);
    }

    private int directionIndex(Vector2 fromDirection)
    {
        Vector2 directionVector = fromDirection.normalized;

        float baseDirectionAngle = 360 / animationDirections.Length;
        float baseDirectionOffset = baseDirectionAngle / 2;
        float directionAngle = Vector2.SignedAngle(Vector2.up, directionVector);

        directionAngle += baseDirectionOffset;

        if (directionAngle < 0)
        {
            directionAngle += 360;
        }

        int stepCount = Mathf.FloorToInt(directionAngle / baseDirectionAngle);
        return stepCount;
    }

}
