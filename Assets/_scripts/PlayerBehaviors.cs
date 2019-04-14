using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviors : MonoBehaviour
{
    public static int health;
    public static int hunger;
    public static int sanity;

    public static List<GameObject> itemsAround;

    public int inventorySpace;
    // Start is called before the first frame update
    void Start()
    {
        health = 50;
        hunger = 50;
        sanity = 50;
        itemsAround = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        //print(itemsAround.Count);
        print("health: " + health);
        print("hunger: " + hunger);
        print("sanity: " + sanity);
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

    }
}
