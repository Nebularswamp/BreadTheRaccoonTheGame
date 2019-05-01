using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    Transform player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        if(player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, 2f * Time.deltaTime);
            transform.Translate(0, 0, -10);
        }
    }
}
