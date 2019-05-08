using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowTarget : MonoBehaviour
{
    Transform player;

    bool isDead = false;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (PlayerBehaviors.health <= 0)
        {
            isDead = true;
            player.gameObject.SetActive(false);
        }

    }

    private void OnGUI()
    {
        if (isDead)
        {
            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 50), "Bread didn't survive!");

            if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height / 2, 300, 100), "Do you want to Continue?"))
            {
                isDead = false;
                PlayerBehaviors.health = 70;
                PlayerBehaviors.hunger = 70;
                PlayerBehaviors.sanity = 70;
                player.gameObject.SetActive(true);
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    SceneManager.LoadScene("StartMenu");
                }
                else if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    player.transform.position = new Vector3(0f, 15f, 0f);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    player.transform.position = new Vector3(-10f, 1f, 0f);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 150, 300, 100), "Do you want to Quit?"))
            {
                Application.Quit();
            }
        }
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
