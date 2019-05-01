using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    //public GameObject instruction;

    public string itemName;
    public Sprite itemSprite;

    //bool interactable;
    public bool canPickUp;

    public List<GameObject> content;
    public List<int> contentNumber;

    public bool ifNew;

    public List<GameObject> usefulItems;

    public List<Image> garbage;
    //public string itemID;

    public GameObject inventory;

    public Image relatedGarbage;

    GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        //print("Apple!");
        inventory = GameObject.Find("Inventory");
        //instruction = GameObject.Find("Instructions");
        //instruction = Inventory.instruction;
        //interactable = false;
        //instruction.SetActive(false);
        if (GetComponent<SpriteRenderer>())
        {
            itemSprite = GetComponent<SpriteRenderer>().sprite;
        }

        ifNew = true;

        if(content.Count > 0)
        {
            InstantiateTrashContent();
            //print(usefulItems.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !Inventory.inventoryOpened)
        {
            //interactable = true;
            //instruction = GameObject.Find("Instructions");
            player = collision.gameObject;
            Inventory.ShowInstruction();
            PlayerBehaviors.itemsAround.Add(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !Inventory.inventoryOpened)
        {
            //interactable = false;
            //instruction = GameObject.Find("Instructions");
            //player = null;
            Inventory.HideInstruction();
            PlayerBehaviors.itemsAround.Remove(gameObject);
        }
    }

    public string GetItemName()
    {
        return itemName;
    }

    public void HideItem()
    {
        if (GetComponent<SpriteRenderer>())
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        if (GetComponent<Collider2D>())
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void ShowItem()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public void RemoveItemFromItemsAround()
    {
        PlayerBehaviors.itemsAround.Remove(gameObject);
    }

    public void DropItem()
    {
        GameObject player = GameObject.Find("Player");
        transform.position = player.transform.position;
        ShowItem();
        Inventory.inventorySpace += 1;
    }

    public void TakeEffect()
    {
        if (gameObject.CompareTag("Food"))
        {
            Animator animator = GameObject.Find("Player").GetComponent<Animator>();
            animator.SetTrigger("eat");

            int[] effects = ItemWiki.ReturnEffects(itemName);

            PlayerBehaviors.health += effects[0];
            PlayerBehaviors.health = Mathf.Clamp(PlayerBehaviors.health, 0, 100);

            PlayerBehaviors.hunger += effects[1];
            PlayerBehaviors.hunger = Mathf.Clamp(PlayerBehaviors.hunger, 0, 100);

            PlayerBehaviors.sanity += effects[2];
            PlayerBehaviors.sanity = Mathf.Clamp(PlayerBehaviors.sanity, 0, 100);

            DestroyItem();
        }

        if (gameObject.CompareTag("Trash"))
        {
            inventory.GetComponent<Inventory>().InstantiateContent(usefulItems);
            inventory.GetComponent<Inventory>().InstantiateContent(garbage);
            Inventory.OpenTrashGame();
            Inventory.GarbageBinOpened = gameObject;
        }

        if (gameObject.CompareTag("TrashCat"))
        {
            GetComponent<Trash_Cat>().Boo();
        }

        if (gameObject.CompareTag("Water"))
        {
            //Animator animator = GameObject.Find("Player").GetComponent<Animator>();
            //animator.SetTrigger("eat");

            int[] effects = ItemWiki.ReturnEffects(itemName);

            PlayerBehaviors.health += effects[0];
            PlayerBehaviors.health = Mathf.Clamp(PlayerBehaviors.health, 0, 100);

            PlayerBehaviors.hunger += effects[1];
            PlayerBehaviors.hunger = Mathf.Clamp(PlayerBehaviors.hunger, 0, 100);

            PlayerBehaviors.sanity += effects[2];
            PlayerBehaviors.sanity = Mathf.Clamp(PlayerBehaviors.sanity, 0, 100);

            //DestroyItem();
        }
    }

    public void AddToInventory()
    {
        if (gameObject.CompareTag("Food") || gameObject.CompareTag("Item"))
        {
            if(Inventory.inventorySpace > 0)
            {
                //Item copy = new Item(); 
                //copy.itemSprite = itemSprite;
                
                for (int i = 0; i < Inventory.inventory.Length; i++)
                {
                    if (Inventory.inventory[i] == null)
                    {
                        //print("to inven");
                        Inventory.inventory[i] = gameObject;
                        print(Inventory.inventory[i]);
                        //Inventory.inventoryItemName[i] = itemName; 
                        Inventory.inventorySpace -= 1;
                        break;
                    }
                }
                GameObject.Find("Inventory").GetComponent<Inventory>().UpdateInventory();
                if (PlayerBehaviors.itemsAround.Contains(gameObject))
                {
                    PlayerBehaviors.itemsAround.Remove(gameObject);
                }

                HideItem();
            }
        }
    }

    public void InstantiateTrashContent()
    {
        for(int i = 0; i < content.Count; i++)
        {
            int count = 0;
            while(count < contentNumber[i])
            {
                GameObject item = Instantiate(content[i], transform.position, transform.rotation);
                item.GetComponent<Item>().HideItem();
                usefulItems.Add(item);
                count += 1;
            }
        }
    }
}
