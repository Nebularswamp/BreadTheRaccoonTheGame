using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    public float timeBetUpdateLetters = 0.1f;

    Text dialogText;
    Image dialogBubble;
    string currentTypingDialgoue;

    private void Start()
    {
        dialogText = GetComponentInChildren<Text>();
        dialogBubble = GetComponentInChildren<Image>();
        HideBubbleText();
    }

    public void HideBubbleText()
    {
        dialogText.enabled = false;
        dialogBubble.enabled = false;
    }

    public void ShowBubbleText(string name)
    {
        if (name == "Player")
            transform.position = ScriptLocator.player.transform.position;
        else if (name == "Mom")
            transform.position = ScriptLocator.mom.transform.position;

        dialogText.enabled = true;
        dialogBubble.enabled = true;
    }

    public bool isTyping
    {
        get; private set;
    }

    public void SkipTypingLetter()
    {
        StopCoroutine("TypeText");
        isTyping = false;
        dialogText.text = currentTypingDialgoue;
    }

    public void SetSay(string name, string dialogue)
    {
        ShowBubbleText(name);

        currentTypingDialgoue = dialogue;

        if (timeBetUpdateLetters <= 0f)
        {
            dialogText.text = dialogue;
        }
        else
        {
            StartCoroutine("TypeText", currentTypingDialgoue);
        }
    }

    public IEnumerator TypeText(string texts)
    {
        isTyping = true;

        dialogText.text = string.Empty;

        foreach (char letter in texts.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(timeBetUpdateLetters);
        }

        isTyping = false;
    }
}
