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
    public static GameObject GarbageBinOpened;
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
        //HighlightSlot(slotSelected);
        //print(slotSelected);
        if (selectedGarbageItem)
        {
            print(selectedGarbageItem);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryOpened = false;
            HideInstruction();
            slotSelected = 0;

            if (trashGameOpened)
            {
                trashGameOpened = false;
                trashSelected = false;
                CloseTrashGame();

                ClearContent();

                GarbageBinOpened = null;
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
                    slotSelected = 0;
                    inventoryOpened = false;
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
                    inventoryOpened = false;
                    slotSelected = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && trashSelected)
        {
            if (selectedGarbageItem)
            {
                selectedGarbageItem.GetComponent<Item>().AddToInventory();
                GarbageBinOpened.GetComponent<Item>().usefulItems.Remove(selectedGarbageItem);
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
                    GarbageBinOpened.GetComponent<Item>().usefulItems.Add(inventory[i]);
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
        trashGame.GetComponent<Image>().enabled = true;
        trashGameOpened = true;
        instruction.SetActive(false);
    }

    public static void CloseTrashGame()
    {
        trashGame.GetComponent<Image>().enabled = false;
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
            newGarbage.rectTransform.localScale = new Vector3(1, 1, 1);
            float posX = trashGame.transform.position.x + Random.Range(-200f, 200f);
            float posY = trashGame.transform.position.y + Random.Range(-200f, 200f);
            newGarbage.transform.position = new Vector3(posX, posY, 0);
        }
    }

    public void InstantiateGarbage(List<Image> item)
    {
        for (int i = 0; i < item.Count; i++)
        {
            int count = 0;
            int amount = Random.Range(0, 7);
            while (count < amount)
            {
                Image newGarbage = Instantiate(item[i]);

                newGarbage.transform.SetParent(trashGame.transform);
                newGarbage.rectTransform.localScale = new Vector3(1, 1, 1);
                float posX = trashGame.transform.position.x + Random.Range(-200f, 200f);
                float posY = trashGame.transform.position.y + Random.Range(-200f, 200f);
                float rotZ = Random.Range(-180f, 180f);
                newGarbage.transform.position = new Vector3(posX, posY, 0);
                newGarbage.transform.rotation = Quaternion.Euler(0, 0, rotZ);
                count += 1;
            }
        }
    }

    public void InstantiateEliteGarbage(List<Image> item)
    {
        for (int i = 0; i < item.Count; i++)
        {
            int count = 0;
            int amount = Random.Range(0, 3);
            while (count < amount)
            {
                Image newGarbage = Instantiate(item[i]);

                newGarbage.transform.SetParent(trashGame.transform);
                newGarbage.rectTransform.localScale = new Vector3(1, 1, 1);
                float posX = trashGame.transform.position.x + Random.Range(-200f, 200f);
                float posY = trashGame.transform.position.y + Random.Range(-200f, 200f);
                float rotZ = Random.Range(-180f, 180f);
                newGarbage.transform.position = new Vector3(posX, posY, 0);
                newGarbage.transform.rotation = Quaternion.Euler(0, 0, rotZ);
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

    void HighlightSlot(int slotNumber)
    {
        print(slotSelected);
        for (int i = 0; i < inventorySpace; i++)
        {
            if(i == slotSelected - 1)
            {
                print("IN");
                slots[i].GetComponentInParent<CanvasGroup>().alpha = 1f;
            }
            else
            {
                slots[i].GetComponentInParent<CanvasGroup>().alpha = 0.4f;
            }
        }
        /*if(slotSelected > 0)
        {
            slots[slotNumber - 1].GetComponentInParent<CanvasGroup>().alpha = 1;
        }*/
    }
}
