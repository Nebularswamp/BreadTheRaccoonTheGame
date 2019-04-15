using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] slots;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            print(slots[i]);
        }
    }

    public void UpdateInventory()
    {

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Image>().enabled = false;
            if (PlayerBehaviors.inventory[i] != null)
            {
                print(slots[i]);
                print(PlayerBehaviors.inventory[i]);
                slots[i].GetComponent<Image>().sprite = PlayerBehaviors.inventory[i];
                slots[i].GetComponent<Image>().enabled = true;
            }
        }
    }
}
