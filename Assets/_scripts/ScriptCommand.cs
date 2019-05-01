using System.Collections;
using UnityEngine;

public abstract class ScriptCommand
{
    public int id = 0;
    public abstract void Invoke();
}
