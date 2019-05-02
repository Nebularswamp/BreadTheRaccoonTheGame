using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSection2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ScriptLocator.scriptParser.isEvent2 = true;
            ScriptLocator.scriptParser.CommandRun();
            Destroy(gameObject);
        }
    }
}
