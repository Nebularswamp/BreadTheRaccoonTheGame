using System.Collections;
using UnityEngine;

public class ScriptMove : ScriptCommand
{
    string name;
    string time;

    public ScriptMove(string _name, string _time)
    {
        name = _name;
        time = _time;
    }

    public override void Invoke()
    {
        if(name == "Player")
            ScriptLocator.player.GetComponent<PlayerAutoMotor>().Move(time);
        else if(name == "Mom")
            ScriptLocator.mom.GetComponent<PlayerAutoMotor>().Move(time);
    }
}
