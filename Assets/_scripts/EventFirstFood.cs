using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFirstFood : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ScriptLocator.scriptParser.CommandRun();
            Destroy(gameObject);
        }
    }
}
