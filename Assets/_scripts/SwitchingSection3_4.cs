using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchingSection3_4 : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.transform.position = new Vector3(-10f, 1f, 0f);
            foreach (GameObject item in Inventory.inventory)
            {
                if (item)
                {
                    DontDestroyOnLoad(item);
                }
            }
            SceneManager.LoadScene("Section4_Survival");
        }
    }
}
