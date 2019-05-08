using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleFreshFirst : MonoBehaviour
{
    bool ishappened = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !ishappened)
        {
            ishappened = true;
            ScriptLocator.scriptParser.CommandRun();
            GetComponent<AppleFreshFirst>().enabled = false;
        }
    }
}
