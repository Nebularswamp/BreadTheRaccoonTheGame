using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptScene : MonoBehaviour
{
    public List<ScriptCommand> commands = new List<ScriptCommand>();
    private IEnumerator<ScriptCommand> iterator;

    public bool isScripting;

    public void AddCommand(ScriptCommand command)
    {
        commands.Add(command);
    }

    public void InvokeCommand()
    {
        if (isScripting)
        {
            if (ScriptLocator.textDisplayer.isTyping)
            {
                ScriptLocator.textDisplayer.SkipTypingLetter();
                return;
            }

            if (ScriptLocator.player.GetComponent<PlayerAutoMotor>().isAutoMoving || ScriptLocator.mom.GetComponent<PlayerAutoMotor>().isAutoMoving)
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
                ScriptLocator.player.GetComponent<PlayerAutoMotor>().isAutoMoving = false;
                ScriptLocator.player.GetComponent<PlayerMovement>().isScripting = false;
                isScripting = false;
                Debug.LogWarning("End of Scene");
            }
        }
    }

    public void InitiateScript()
    {
        iterator = null;
        iterator = commands.GetEnumerator();
        isScripting = true;
    }
}