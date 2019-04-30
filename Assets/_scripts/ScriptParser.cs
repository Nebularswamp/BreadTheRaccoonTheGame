using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGPrompter;

public class ScriptParser : MonoBehaviour
{
    Script script;
    ScriptScene scene = new ScriptScene();

    bool isLoading = false;

    GameObject event1;
    GameObject event2;

    bool isEvent1 = false;

    void Start()
    {
        isLoading = true;
        ScriptLocator.mom.GetComponent<PlayerMovement>().isScripting = true;
        script = Script.FromSource("Assets/_scripts/CutSceneScripts/scene1.sce");
        script.Prime();
        script.Validate();
        event1 = GameObject.Find("Apple_Fresh_event1");
        event2 = GameObject.Find("Apple_Fresh_event2");
        StartCoroutine(script.GetCurrentEnumerator(OnLine, SelectChoice, OnChoiceSelected, OnReturn));
    }


    public void StartScript(string file)
    {
        isLoading = true;
        ScriptLocator.mom.GetComponent<PlayerMovement>().isScripting = true;
        script = Script.FromSource("Assets/_scripts/CutSceneScripts/" + file);
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
        else if(line.Tag.Substring(0, 1) == "e")
        {
            scene.AddCommand(new ScriptEventSay(line.Tag.Substring(1), line.Text));
            yield break;
        }
        else if (line.Tag == "pause")
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

        if (!isEvent1)
        {
            if(event1 == null && event2 == null)
            {
                isEvent1 = true;
                CommandRun();
            }
            else if(event1 == null&& !event2.GetComponent<SpriteRenderer>().enabled)
            {
                isEvent1 = true;
                CommandRun();
            }
            else if (event2 == null && !event1.GetComponent<SpriteRenderer>().enabled)
            {
                isEvent1 = true;
                CommandRun();
            }
            else if((event1 != null && event2 != null)&&(!event1.GetComponent<SpriteRenderer>().enabled&& !event2.GetComponent<SpriteRenderer>().enabled))
            {
                isEvent1 = true;
                CommandRun();
            }
        }
    }

    public void CommandRun()
    {
        ScriptLocator.player.GetComponent<PlayerMovement>().isScripting = true;
        ScriptLocator.mom.GetComponent<PlayerAutoMotor>().isAutoMoving = false;
        scene.isScripting = true;
        ScriptLocator.player.GetComponent<PlayerMovement>().movement = Vector3.zero;
        scene.InvokeCommand();
    }

    public void CommandEvent(int num)
    {
        var eventRun = scene.commands.Find(x => x.id == num);
        ScriptLocator.player.GetComponent<PlayerMovement>().isScripting = true;
        ScriptLocator.player.GetComponent<PlayerMovement>().movement = Vector3.zero;
        eventRun.Invoke();
        StartCoroutine(HideBubbleTextCo());
    }

    IEnumerator HideBubbleTextCo()
    {
        yield return new WaitForSeconds(2.5f);
        ScriptLocator.textDisplayer.HideBubbleText();
        PlayerMovement.isHurt = false;
        ScriptLocator.player.GetComponent<PlayerMovement>().isScripting = false;
    }

    public void PauseScript()
    {
        ScriptLocator.textDisplayer.HideBubbleText();
        ScriptLocator.player.GetComponent<PlayerAutoMotor>().isAutoMoving = false;
        scene.isScripting = false;
    }
}
