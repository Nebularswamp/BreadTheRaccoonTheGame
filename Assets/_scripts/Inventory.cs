using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] slots;

    public Image garbage;

    public static GameObject instruction;
    public static GameObject trashGame;

    //public static Sprite[] inventoryItemImage;
    //public static string[] inventoryItemName;
    public static GameObject[] inventory;
    public static int inventorySpace;

    public static bool inventoryOpened;
    public static bool trashGameOpened;
    public static bool trashSelected;

    int slotSelected;
    public static GameObject selectedGarbageItem;
    // Start is called before the first frame update
    void Start()
    {
        slotSelected = 0;
        inventorySpace = 4;

        instruction = GameObject.Find("Instructions");
        trashGame = GameObject.Find("InsideTrash");

        HideInstruction();
        CloseTrashGame();

        //inventoryItemImage = new Sprite[inventorySpace];
        //inventoryItemName = new string[inventorySpace];
        inventory = new GameObject[inventorySpace];

        inventoryOpened = false;
        trashGameOpened = false;
        trashSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        //print(inventorySpace);
        if (selectedGarbageItem)
        {
            print(selectedGarbageItem);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(slotSelected > 0 && trashGameOpened)
            {
                inventoryOpened = false;
                HideInstruction();
                slotSelected = 0;
            }
            else
            {
                inventoryOpened = false;
                HideInstruction();
                slotSelected = 0;

                trashGameOpened = false;
                trashSelected = false;
                CloseTrashGame();

                ClearContent();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(inventory[0] != null)
            {
                instruction.SetActive(true);
                inventoryOpened = true;
                trashSelected = false;
                slotSelected = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (inventory[1] != null)
            {
                instruction.SetActive(true);
                inventoryOpened = true;
                trashSelected = false;
                slotSelected = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (inventory[2] != null)
            {
                instruction.SetActive(true);
                inventoryOpened = true;
                trashSelected = false;
                slotSelected = 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (inventory[3] != null)
            {
                instruction.SetActive(true);
                inventoryOpened = true;
                trashSelected = false;
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
                    if (inventory[i].GetComponent<Item>().relatedGarbage)
                    {
                        inventory[i].GetComponent<Item>().relatedGarbage.GetComponent<Garbage>().DestroyGarbage();
                    }
                    inventorySpace += 1;
                    //print(inventory[i]);
                    inventory[i] = null;
                    UpdateInventory();
                    instruction.SetActive(false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && trashSelected)
        {
            //print("IN");
            if (selectedGarbageItem)
            {
                selectedGarbageItem.GetComponent<Item>().TakeEffect();
                selectedGarbageItem.GetComponent<Item>().relatedGarbage.GetComponent<Garbage>().DestroyGarbage();
                instruction.SetActive(false);
                trashSelected = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && inventoryOpened && !trashGameOpened)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slotSelected == (i + 1) && inventory[i])
                {
                    inventory[i].GetComponent<Item>().DropItem();
                    //print(inventory[i]);
                    inventory[i] = null;
                    UpdateInventory();
                    instruction.SetActive(false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && trashSelected)
        {
            if (selectedGarbageItem)
            {
                selectedGarbageItem.GetComponent<Item>().AddToInventory();
                selectedGarbageItem.GetComponent<Item>().relatedGarbage.GetComponent<Garbage>().HideGarbage();
                instruction.SetActive(false);
                trashSelected = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && inventoryOpened && trashGameOpened)
        {
            //print("IN");
            for (int i = 0; i < slots.Length; i++)
            {
                print("drop " + inventory[i]);
                if (slotSelected == (i + 1) && inventory[i])
                {
                    //print("IN");
                    inventory[i].GetComponent<Item>().relatedGarbage.GetComponent<Garbage>().ShowGarbage();
                    inventorySpace += 1;
                    //print(inventory[i]);
                    inventory[i] = null;
                    UpdateInventory();
                    instruction.SetActive(false);
                    trashSelected = false;
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

    public static void OpenTrashGame()
    {
        trashGame.SetActive(true);
        trashGameOpened = true;
        instruction.SetActive(false);
    }

    public static void CloseTrashGame()
    {
        trashGame.SetActive(false);
    }

    public void InstantiateContent(List<GameObject> item)
    {
        for (int i = 0; i < item.Count; i++)
        {
            Image newGarbage = Instantiate(garbage);
            newGarbage.GetComponent<Garbage>().relatedItem = item[i];
            item[i].GetComponent<Item>().relatedGarbage = newGarbage;
            newGarbage.GetComponent<Image>().sprite = item[i].GetComponent<SpriteRenderer>().sprite;
            newGarbage.transform.SetParent(trashGame.transform);
            float posX = trashGame.transform.position.x + Random.Range(-200f, 200f);
            float posY = trashGame.transform.position.y + Random.Range(-200f, 200f);
            newGarbage.transform.position = new Vector3(posX, posY, 0);
        }
    }

    public void InstantiateContent(List<Image> item)
    {
        for (int i = 0; i < item.Count; i++)
        {
            int count = 0;
            int amount = Random.Range(10, 16);
            while (count < amount)
            {
                Image newGarbage = Instantiate(item[i]);

                newGarbage.transform.SetParent(trashGame.transform);
                float posX = trashGame.transform.position.x + Random.Range(-200f, 200f);
                float posY = trashGame.transform.position.y + Random.Range(-200f, 200f);
                newGarbage.transform.position = new Vector3(posX, posY, 0);
                count += 1;
            }
        }
    }

    public void ClearContent()
    {
        foreach(Transform item in trashGame.transform)
        {
            Destroy(item.gameObject);
        }
    }
}
