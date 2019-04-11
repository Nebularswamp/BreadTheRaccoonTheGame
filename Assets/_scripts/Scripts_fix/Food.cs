using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public string foodName;

    public int healthGain;
    public int hungerGain;
    public int SanityGain;

    public bool inRange;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            inRange = false;
            Debug.Log("Not in range");
        }
    }

    void Update()
    {
        Eat();
    }

    void Eat()
    {
        if (inRange && Input.GetKey(KeyCode.E))
        {
            PlayerStats.instance.health += healthGain;
            PlayerStats.instance.UpdateStats();
            PlayerController.instance.isEating = true;
        }
    }
}
