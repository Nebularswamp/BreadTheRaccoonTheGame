using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGPrompter;

public class ScriptParser : MonoBehaviour
{
    Script script;
    ScriptScene scene = new ScriptScene();

    bool isLoading = false;

    void Start()
    {
        isLoading = true;
        ScriptLocator.mom.GetComponent<PlayerMovement>().isScripting = true;

        script = Script.FromSource("Assets/_scripts/CutSceneScripts/scene1.sce");
        script.Prime();
        script.Validate();
        StartCoroutine(script.GetCurrentEnumerator(OnLine, SelectChoice, OnChoiceSelected, OnReturn));
    }

    IEnumerator OnReturn()
    {
        Debug.Log("Scene End");
        yield return null;
        //        yield return new WaitForSecondsRealtime(0.5f);
        isLoading = false;
//        scene.isScripting = true;
//        scene.InvokeCommand();
    }

    IEnumerator OnChoiceSelected(Script.Menu menu, Script.Choice choice)
    {
        yield return new WaitForSecondsRealtime(2f);
    }

    IEnumerator<int?> SelectChoice(Script.Menu menu)
    {
        yield return menu.TrueChoices[0].Index;
    }

    IEnumerator OnLine(Script.DialogueLine line)
    {
//        Debug.Log(line.Tag);
//        Debug.Log(line.Text);

        if (line.Tag.Substring(0,2) == "mv")
        {
            scene.AddCommand(new ScriptMove(line.Tag.Substring(2), line.Text));
            yield break;
        }
        if (line.Tag == "pause")
        {
            scene.AddCommand(new ScriptPause(line.Text));
            yield break;
        }

        scene.AddCommand(new ScriptSay(line.Tag, line.Text));

        yield return null;
    }

    private void Update()
    {
        if(Input.anyKeyDown&&!isLoading&&scene.isScripting)
        {
            scene.InvokeCommand();
        }
    }

    public void CommandRun()
    {
        ScriptLocator.player.GetComponent<PlayerMovement>().isScripting = true;
        scene.isScripting = true;
        scene.InvokeCommand();
    }

    public void PauseScript()
    {
        ScriptLocator.textDisplayer.HideBubbleText();
        ScriptLocator.player.GetComponent<PlayerAutoMotor>().isAutoMoving = false;
        scene.isScripting = false;
    }
}
