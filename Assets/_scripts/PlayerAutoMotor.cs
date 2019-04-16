using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoMotor : MonoBehaviour
{
    PlayerMovement player;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    public bool isMoving
    {
        get; set;
    }

    public void Move(string time)
    {
        ScriptLocator.textDisplayer.HideBubbleText();
        isMoving = true;
        string first = time.Substring(0, 1);
        Vector3 move = Vector3.zero;

        switch (first)
        {
            case "r":
                move.x = 1.0f;
                break;
            case "l":
                move.x = -1.0f;
                break;
            case "u":
                move.y = 1.0f;
                break;
            case "d":
                move.y = -1.0f;
                break;
        }
        player.MoveMotor(move, float.Parse(time.Substring(1)));
    }
}
