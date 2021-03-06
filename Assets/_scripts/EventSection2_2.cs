﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSection2_2 : MonoBehaviour
{
    GameObject cat1;
    GameObject cat2;
    GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        cat1 = GameObject.Find("Douchebag_Kid1");
        cat2 = GameObject.Find("Douchebag_Kid2");
        cat1.SetActive(false);
        cat2.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cat1.SetActive(true);
            cat2.SetActive(true);
            cat1.GetComponent<BoxCollider2D>().enabled = false;
            cat2.GetComponent<BoxCollider2D>().enabled = false;
            mainCamera.transform.position = cat1.transform.position + Vector3.up*4f;
            Destroy(gameObject);
        }
    }
}
