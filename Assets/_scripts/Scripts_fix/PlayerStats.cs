using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int health = 100;
    public int hunger = 100;
    public int sanity = 100;

    public Text displayHealth;


    
    void Awake()
    {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        
    }

    public void UpdateStats()
    {
        displayHealth.text = "" + health;
    }

}
