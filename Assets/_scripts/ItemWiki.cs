using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWiki : MonoBehaviour
{
    public static Dictionary<string, int[]> wiki = new Dictionary<string, int[]>();

    private void Start()
    {
        //For the int[], the represented statuses are health/hunger/sanity, in this specific order.
        wiki.Add("apple_fresh", new int[] { 5, 20, 5 });
        wiki.Add("apple_moldy", new int[] { 5, 20, 5 });
        wiki.Add("apple_spoiled", new int[] { 5, 20, 5 });
        wiki.Add("chocopie_fresh", new int[] { 5, 20, 5 });
        wiki.Add("chocopie_moldy", new int[] { 5, 20, 5 });
        wiki.Add("chocopie_spoiled", new int[] { 5, 20, 5 });
        wiki.Add("baozi_fresh", new int[] { 5, 20, 5 });
        wiki.Add("baozi_moldy", new int[] { 5, 20, 5 });
        wiki.Add("baozi_spoiled", new int[] { 5, 20, 5 });
        wiki.Add("macaroon_fresh", new int[] { 5, 20, 5 });
        wiki.Add("macaroon_moldy", new int[] { 5, 20, 5 });
        wiki.Add("macaroon_spoiled", new int[] { 5, 20, 5 });



        //Traps and enemies
        wiki.Add("nails", new int[] { -10, 0, 0 });
        wiki.Add("bull_dog", new int[] { -10, 0, 0 });

    }

    public static int[] ReturnEffects(string itemName)
    {
        print(itemName);
        if (wiki.ContainsKey(itemName))
        {
            print("exist");
            return wiki[itemName];
        }
        else
        {
            return null;
       }
    }
}
