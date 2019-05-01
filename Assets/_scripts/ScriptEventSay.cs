using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEventSay : ScriptCommand
{
    string name;
    string dialogue;

    public ScriptEventSay(string _name, string _dialogue)
    {
        id = int.Parse(_name.Substring(0, 1));
        name = _name.Substring(1);
        dialogue = _dialogue;
    }

    public override void Invoke()
    {
        ScriptLocator.textDisplayer.SetSay(name, dialogue);
    }
}
