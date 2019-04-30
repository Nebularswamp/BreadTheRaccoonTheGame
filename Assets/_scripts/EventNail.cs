using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNail : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //            ScriptLocator.scriptParser.CommandEvent(1);
            ScriptLocator.textDisplayer.SetSay("Mom", "What did I tell you?");
            ScriptLocator.player.GetComponent<PlayerMovement>().isScripting = true;
            ScriptLocator.player.GetComponent<PlayerMovement>().movement = Vector3.zero;
            PlayerMovement.isHurt = true;
            StartCoroutine(HideBubbleTextCo());
        }
    }

    IEnumerator HideBubbleTextCo()
    {
        yield return new WaitForSeconds(2.0f);
        ScriptLocator.textDisplayer.HideBubbleText();
        PlayerMovement.isHurt = false;
        ScriptLocator.player.GetComponent<PlayerMovement>().isScripting = false;
    }
}
