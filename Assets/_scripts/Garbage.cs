using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Garbage : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerClickHandler
{
    public GameObject relatedItem;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Inventory.inventoryOpened = false;
        if (gameObject.CompareTag("Item"))
        {
            Inventory.ShowInstruction();
            Inventory.selectedGarbageItem = null;
            Inventory.selectedGarbageItem = relatedItem;
            Inventory.trashSelected = true;
            //print("selected");
        }
        else
        {
            Inventory.HideInstruction();
            Inventory.selectedGarbageItem = null;
            Inventory.trashSelected = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = new Vector2(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y);

        transform.position = new Vector2(transform.position.x + delta.x, transform.position.y + delta.y);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Inventory.inventoryOpened = false;
        if (gameObject.CompareTag("Item"))
        {
            Inventory.ShowInstruction();
            Inventory.selectedGarbageItem = null;
            Inventory.selectedGarbageItem = relatedItem;
            Inventory.trashSelected = true;
        }
        else
        {
            Inventory.HideInstruction();
            Inventory.selectedGarbageItem = null;
            Inventory.trashSelected = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (relatedItem)
        {
            print(relatedItem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyGarbage()
    {
        Destroy(gameObject);
    }

    public void HideGarbage()
    {
        GetComponent<Image>().enabled = false;
    }

    public void ShowGarbage()
    {
        //print("IN");
        GetComponent<Image>().enabled = true;
    }
}
