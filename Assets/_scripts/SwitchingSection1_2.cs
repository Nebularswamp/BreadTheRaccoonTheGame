using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingSection1_2 : MonoBehaviour
{
    GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            mainCamera.AddComponent<FollowTarget>();
            ScriptLocator.player.GetComponent<PlayerMovement>().movement = Vector3.zero;
            ScriptLocator.scriptParser.CommandRun();
            Destroy(gameObject);
        }
    }
}
