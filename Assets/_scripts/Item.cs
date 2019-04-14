using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject instruction;

    public string itemName;

    bool interactable;
    public bool canPickUp;

    // Start is called before the first frame update
    void Start()
    {
        interactable = false;
        instruction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactable = true;
            instruction.SetActive(true);
            PlayerBehaviors.itemsAround.Add(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactable = false;
            instruction.SetActive(false);
            PlayerBehaviors.itemsAround.Remove(gameObject);
        }
    }

    public string GetItemName()
    {
        return itemName;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public void TakeEffect()
    {
        if (gameObject.CompareTag("Food"))
        {
            int[] effects = ItemWiki.ReturnEffects(itemName);
            PlayerBehaviors.health += effects[0];
            PlayerBehaviors.hunger += effects[1];
            PlayerBehaviors.sanity += effects[2];
            PlayerBehaviors.itemsAround.Remove(gameObject);
            DestroyItem();
        }
    }
}
