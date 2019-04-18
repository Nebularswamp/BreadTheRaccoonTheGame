using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] slots;
    public GameObject instruction;

    public static Sprite[] inventoryItemImage;
    public static string[] inventoryItemName;
    public static int inventorySpace;

    public static bool inventoryOpened;
    // Start is called before the first frame update
    void Start()
    {
        inventorySpace = 4;
        inventoryItemImage = new Sprite[inventorySpace];
        inventoryItemName = new string[inventorySpace];
        inventoryOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            print(slots[i]);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryOpened = false;
            instruction.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !inventoryOpened)
        {
            if(inventoryItemImage[0] != null)
            {
                instruction.SetActive(true);
                inventoryOpened = true;
            }
        }
    }

    public void UpdateInventory()
    {

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Image>().enabled = false;
            if (inventoryItemImage[i] != null)
            {
                //print(slots[i]);
               //print(inventoryItemImage[i]);
                slots[i].GetComponent<Image>().sprite = inventoryItemImage[i];
                slots[i].GetComponent<Image>().enabled = true;
            }
        }
    }
}
