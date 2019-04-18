using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviors : MonoBehaviour
{
    public static int health;
    public static int hunger;
    public static int sanity;

    public static List<GameObject> itemsAround;

    public static Sprite[] inventory;
    public static int inventorySpace;
    // Start is called before the first frame update
    void Start()
    {
        health = 50;
        hunger = 50;
        sanity = 50;

        inventorySpace = 4;

        itemsAround = new List<GameObject>();
        inventory = new Sprite[4];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PickUp();
        }

        //print(itemsAround.Count);
        print("health: " + health);
        print("hunger: " + hunger);
        print("sanity: " + sanity);
        print(inventorySpace);
    }

    private void Interact()
    {
        GameObject[] itemsArray = itemsAround.ToArray();
        for (int i = 0; i < itemsArray.Length; i++)
        {
            itemsArray[i].GetComponent<Item>().TakeEffect();
        }
    }

    private void PickUp()
    {
        GameObject[] itemsArray = itemsAround.ToArray();
        for (int i = 0; i < itemsArray.Length; i++)
        {
            itemsArray[i].GetComponent<Item>().AddToInventory();
        }
    }
}
