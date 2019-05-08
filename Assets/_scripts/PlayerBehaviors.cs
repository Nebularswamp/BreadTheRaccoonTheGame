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

    public Text healthText;
    public Text hungerText;
    public Text sanityText;

    public GameObject effectsPanel;

    void Start()
    {
        health = 70;
        hunger = 70;
        sanity = 70;

        bleed_UI.GetComponent<Image>().enabled = false;
        paranoid_UI.GetComponent<Image>().enabled = false;

        itemsAround = new List<GameObject>();

        InvokeRepeating("ReduceHunger", 0f, 1.5f);
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

//        print(PlayerMovement.isHurt);
        //print(itemsAround.Count);
  //      print("health: " + health);
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
        int preHealth = health;
        int preHunger = hunger;
        int preSanity = sanity;
        GameObject[] itemsArray = itemsAround.ToArray();

        if(itemsArray.Length > 0)
        {
            if (itemsArray[0].GetComponent<Item>().memory.Length > 0)
            {
                StartCoroutine(ShowMemory());
            }
        }

        IEnumerator ShowMemory()
        {
            GameObject mem = GameObject.Find("Memory");
            GameObject memContent = GameObject.Find("MemoryContent");
            mem.GetComponent<CanvasGroup>().alpha = 1;
            string text = itemsArray[0].GetComponent<Item>().memory;
            memContent.GetComponent<Text>().text = null;

            foreach (char letter in text.ToCharArray())
            {
                memContent.GetComponent<Text>().text += letter;
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(4f);
            mem.GetComponent<CanvasGroup>().alpha = 0;
        }

        for (int i = 0; i < itemsArray.Length; i++)
        {
            itemsArray[i].GetComponent<Item>().TakeEffect();
            itemsArray[i].GetComponent<Item>().RemoveItemFromItemsAround();
        }

        IEnumerator ShowEffects()
        {
            effectsPanel.GetComponent<CanvasGroup>().alpha = 1;
            yield return new WaitForSeconds(1.5f);
            effectsPanel.GetComponent<CanvasGroup>().alpha = 0;
        }

        healthText.text = "" + (health - preHealth);
        hungerText.text = "" + (hunger - preHunger);
        sanityText.text = "" + (sanity - preSanity);

        StartCoroutine(ShowEffects());
    }

    private void PickUp()
    {
        GameObject[] itemsArray = itemsAround.ToArray();
        for (int i = 0; i < itemsArray.Length; i++)
        {
            if (itemsArray[i].GetComponent<Item>().canPickUp)
            {
                itemsArray[i].GetComponent<Item>().AddToInventory();
                GameObject pickUpAudio = GameObject.Find("PickUpAudio");
                pickUpAudio.GetComponent<AudioSource>().Play();
            }
        }
    }

    private void ReduceHunger()
    {
        hunger -= 1;
    }


}
