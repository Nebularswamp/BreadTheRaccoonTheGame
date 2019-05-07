using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSection2_3 : MonoBehaviour
{
    GameObject player;
    GameObject canvas;

    private void Start()
    {
        player = GameObject.Find("Player");
        canvas = GameObject.Find("Canvas");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(canvas);
            SceneManager.LoadScene("Section3_Chase");
        }
    }
}
