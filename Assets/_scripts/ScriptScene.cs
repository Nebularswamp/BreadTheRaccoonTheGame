using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptScene : MonoBehaviour
{
    public List<ScriptCommand> commands = new List<ScriptCommand>();
    private IEnumerator<ScriptCommand> iterator;

    public void AddCommand(ScriptCommand command)
    {
        commands.Add(command);
    }

    public void InvokeCommand()
    {
        if (ScriptLocator.textDisplayer.isTyping)
        {
            ScriptLocator.textDisplayer.SkipTypingLetter();
            return;
        }

        if (ScriptLocator.player.GetComponent<PlayerAutoMotor>().isMoving || ScriptLocator.mom.GetComponent<PlayerAutoMotor>().isMoving)
        {
            return;
        }

        if (iterator == null)
        {
            InitiateScript();
        }

        if (iterator.MoveNext())
        {
            iterator.Current.Invoke();
        }
        else
        {
            ScriptLocator.textDisplayer.HideBubbleText();
            ScriptLocator.player.GetComponent<PlayerMovement>().isAnimation = false;
            Debug.LogWarning("End of Scene");
        }
    }

    public void InitiateScript()
    {
        iterator = null;
        iterator = commands.GetEnumerator();
    }
}