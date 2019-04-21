using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] slots;

    public static GameObject instruction;

    //public static Sprite[] inventoryItemImage;
    //public static string[] inventoryItemName;
    public static GameObject[] inventory;
    public static int inventorySpace;

    public static bool inventoryOpened;

    int slotSelected;
    // Start is called before the first frame update
    void Start()
    {
        slotSelected = 0;
        instruction = GameObject.Find("Instructions");
        HideInstruction();
        inventorySpace = 4;
        //inventoryItemImage = new Sprite[inventorySpace];
        //inventoryItemName = new string[inventorySpace];
        inventory = new GameObject[inventorySpace];
        inventoryOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryOpened = false;
            instruction.SetActive(false);
            slotSelected = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(inventory[0] != null)
            {
                instruction.SetActive(true);
                inventoryOpened = true;
                slotSelected = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (inventory[1] != null)
            {
                instruction.SetActive(true);
                inventoryOpened = true;
                slotSelected = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (inventory[2] != null)
            {
                instruction.SetActive(true);
                inventoryOpened = true;
                slotSelected = 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (inventory[3] != null)
            {
                instruction.SetActive(true);
                inventoryOpened = true;
                slotSelected = 4;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && inventoryOpened)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slotSelected == (i+1) && inventory[i])
                {
                    inventory[i].GetComponent<Item>().TakeEffect();
                    //print(inventory[i]);
                    inventory[i] = null;
                    UpdateInventory();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && inventoryOpened)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slotSelected == (i + 1) && inventory[i])
                {
                    inventory[i].GetComponent<Item>().DropItem();
                    //print(inventory[i]);
                    inventory[i] = null;
                    UpdateInventory();
                }
            }
        }
    }

    public void UpdateInventory()
    {

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Image>().enabled = false;
            if (inventory[i] != null)
            {
                //print(slots[i]);
               //print(inventoryItemImage[i]);
                slots[i].GetComponent<Image>().sprite = inventory[i].GetComponent<SpriteRenderer>().sprite;
                slots[i].GetComponent<Image>().enabled = true;
            }
        }
    }

    public static void ShowInstruction()
    {
        instruction.SetActive(true);
    }

    public static void HideInstruction()
    {
        instruction.SetActive(false);
    }
}
