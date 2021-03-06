﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWiki : MonoBehaviour
{
    public static Dictionary<string, int[]> wiki = new Dictionary<string, int[]>();

    private void Start()
    {
        //For the int[], the represented statuses are health/hunger/sanity, in this specific order.
        wiki.Add("apple_fresh", new int[] { 10, 25, 10 });
        wiki.Add("apple_moldy", new int[] { 0, 20, 0 });
        wiki.Add("apple_spoiled", new int[] { -2, 20, -2 });
        wiki.Add("chocopie_fresh", new int[] { 5, 35, 30 });
        wiki.Add("chocopie_moldy", new int[] { 0, 25, 0 });
        wiki.Add("chocopie_spoiled", new int[] { -2, 25, -5 });
        wiki.Add("baozi_fresh", new int[] { 10, 25, 10 });
        wiki.Add("baozi_moldy", new int[] { 0, 20, 0 });
        wiki.Add("baozi_spoiled", new int[] { -2, 20, -15 });
        wiki.Add("macaroon_fresh", new int[] { 5, 15, 10 });
        wiki.Add("macaroon_moldy", new int[] { 0, 15, 0 });
        wiki.Add("macaroon_spoiled", new int[] { -2, 5, -3 });

        wiki.Add("puddle_clean", new int[] { 10, 0, 5 });



        //Traps and enemies
        wiki.Add("nails", new int[] { -5, 0, -5 });
        wiki.Add("bull_dog", new int[] { -15, 0, -10 });
        wiki.Add("stray_cat", new int[] { -10, 0, -5 });
        wiki.Add("douchebag_kid", new int[] { -10, 0, -5 });
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
