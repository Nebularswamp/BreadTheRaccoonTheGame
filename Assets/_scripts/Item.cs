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
    public List<Image> eliteGarbage;
    //public string itemID;

    public GameObject inventory;

    public Image relatedGarbage;

    GameObject player;

    public string memory;
    
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
        player = GameObject.Find("Player");
        transform.position = player.transform.position;
        ShowItem();
        Inventory.inventorySpace += 1;
    }

    public void TakeEffect()
    {
        int preHealth = PlayerBehaviors.health;
        int preHunger = PlayerBehaviors.hunger;
        int preSanity = PlayerBehaviors.sanity;



        if (memory.Length > 0)
        {
            StartCoroutine(ShowMemory());
        }
    

        IEnumerator ShowMemory()
        {
            GameObject mem = GameObject.Find("Memory");
            GameObject memContent = GameObject.Find("MemoryContent");
            mem.GetComponent<CanvasGroup>().alpha = 1;
            string text = memory;
            memContent.GetComponent<Text>().text = null;

            foreach (char letter in text)
            {
                memContent.GetComponent<Text>().text += letter;
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(3f);
            mem.GetComponent<CanvasGroup>().alpha = 0;
        }

        GameObject effectsPanel = GameObject.Find("Effects");
        GameObject healthText = GameObject.Find("HealthText");
        GameObject hungerText = GameObject.Find("HungerText");
        GameObject sanityText = GameObject.Find("SanityText");

        IEnumerator ShowEffects()
        {
            effectsPanel.GetComponent<CanvasGroup>().alpha = 1;
            yield return new WaitForSeconds(1.5f);
            effectsPanel.GetComponent<CanvasGroup>().alpha = 0;
        }

        if (gameObject.CompareTag("Food"))
        {
            player = GameObject.Find("Player");
            Animator animator = player.GetComponent<Animator>();
            animator.SetTrigger("eat");

            //HideItem();

            int[] effects = ItemWiki.ReturnEffects(itemName);

            PlayerBehaviors.health += effects[0];
            PlayerBehaviors.health = Mathf.Clamp(PlayerBehaviors.health, 0, 100);

            PlayerBehaviors.hunger += effects[1];
            PlayerBehaviors.hunger = Mathf.Clamp(PlayerBehaviors.hunger, 0, 100);

            PlayerBehaviors.sanity += effects[2];
            PlayerBehaviors.sanity = Mathf.Clamp(PlayerBehaviors.sanity, 0, 100);

            StartCoroutine(PlayAudio());
        }

        if (gameObject.CompareTag("Trash"))
        {
            inventory.GetComponent<Inventory>().InstantiateContent(usefulItems);
            inventory.GetComponent<Inventory>().InstantiateGarbage(garbage);
            inventory.GetComponent<Inventory>().InstantiateEliteGarbage(eliteGarbage);
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

        healthText.GetComponent<Text>().text = "" + (PlayerBehaviors.health - preHealth);
        hungerText.GetComponent<Text>().text = "" + (PlayerBehaviors.hunger - preHunger);
        sanityText.GetComponent<Text>().text = "" + (PlayerBehaviors.sanity - preSanity);

        StartCoroutine(ShowEffects());
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

    IEnumerator PlayAudio()
    {
        HideItem();
        GetComponent<AudioSource>().Play();
        print("playaudio");
        yield return new WaitForSeconds(10f);
        DestroyItem();
    }

}
