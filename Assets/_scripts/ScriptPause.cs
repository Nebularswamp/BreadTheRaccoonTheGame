using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPause : ScriptCommand
{
    string name;

    public ScriptPause(string _name)
    {
        name = _name;
    }

    public override void Invoke()
    {
        ScriptLocator.scriptParser.PauseScript();

        if (name == "Player")
            ScriptLocator.player.GetComponent<PlayerMovement>().isScripting = false;
        else if (name == "Mom")
            ScriptLocator.player.GetComponent<PlayerMovement>().isScripting = false;
    }
}
