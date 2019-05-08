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
            player.transform.position = new Vector3(0f, 15f, 0f);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(canvas);
            foreach(GameObject item in Inventory.inventory)
            {
                if (item)
                {
                    DontDestroyOnLoad(item);
                }
            }
            SceneManager.LoadScene("Section3_Chase");
        }
    }
}
