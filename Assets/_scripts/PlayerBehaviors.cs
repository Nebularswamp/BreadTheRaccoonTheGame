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

    public GameObject bleed_UI;
    public GameObject darkness_UI;
    public GameObject paranoid_UI;


    // Start is called before the first frame update
    void Start()
    {
        health = 70;
        hunger = 70;
        sanity = 70;

        bleed_UI.GetComponent<Image>().enabled = false;
        paranoid_UI.GetComponent<Image>().enabled = false;

        itemsAround = new List<GameObject>();

        InvokeRepeating("ReduceHunger", 0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !Inventory.inventoryOpened)
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.Q) && !Inventory.inventoryOpened)
        {
            PickUp();
        }

        print(PlayerMovement.isHurt);
        //print(itemsAround.Count);
        print("health: " + health);
        //print("hunger: " + hunger);
        //print("sanity: " + sanity);
        //print(inventorySpace);

        if (health < 30)
        {
            bleed_UI.GetComponent<Image>().enabled = true;
        }
        else
        {
            bleed_UI.GetComponent<Image>().enabled = false;
        }

        if (sanity < 30)
        {
            paranoid_UI.GetComponent<Image>().enabled = true;
        }
        else
        {
            paranoid_UI.GetComponent<Image>().enabled = false;
        }

    }

    private void Interact()
    {
        GameObject[] itemsArray = itemsAround.ToArray();
        for (int i = 0; i < itemsArray.Length; i++)
        {
            itemsArray[i].GetComponent<Item>().TakeEffect();
            itemsArray[i].GetComponent<Item>().RemoveItemFromItemsAround();
        }
    }

    private void PickUp()
    {
        GameObject[] itemsArray = itemsAround.ToArray();
        for (int i = 0; i < itemsArray.Length; i++)
        {
            if (itemsArray[i].GetComponent<Item>().canPickUp)
            {
                itemsArray[i].GetComponent<Item>().AddToInventory();
            }
        }
    }

    private void ReduceHunger()
    {
        hunger -= 1;
    }
}
