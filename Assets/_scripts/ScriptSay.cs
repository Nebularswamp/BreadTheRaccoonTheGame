using System.Collections;
using UnityEngine;

public class ScriptSay : ScriptCommand
{
    string name;
    string dialogue;

    public ScriptSay(string _name, string _dialogue)
    {
        name = _name;
        dialogue = _dialogue;
    }

    public override void Invoke()
    {
        ScriptLocator.textDisplayer.SetSay(name, dialogue);
    }
}
