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
        script = Script.FromSource("Assets/_scripts/CutSceneScripts/scene1.sce");
        script.Prime();
        script.Validate();
        StartCoroutine(script.GetCurrentEnumerator(OnLine, SelectChoice, OnChoiceSelected, OnReturn));
    }

    IEnumerator OnReturn()
    {
//        Debug.Log("Scene End");
        yield return new WaitForSecondsRealtime(0.5f);
        isLoading = false;
        scene.InvokeCommand();
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

        scene.AddCommand(new ScriptSay(line.Tag, line.Text));

        yield return null;
    }

    private void Update()
    {
        if(Input.anyKeyDown&&!isLoading)
        {
            scene.InvokeCommand();
        }
    }

    public void CommandRun()
    {
        scene.InvokeCommand();
    }
}
