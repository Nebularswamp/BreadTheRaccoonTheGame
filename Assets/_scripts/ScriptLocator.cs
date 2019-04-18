using System.Collections;
using UnityEngine;

public class ScriptLocator
{
    private static ScriptParser _scriptParser;
    public static ScriptParser scriptParser
    {
        get
        {
            if (!_scriptParser)
            {
                _scriptParser = GameObject.FindObjectOfType<ScriptParser>();
            }

            return _scriptParser;
        }
    }

    private static TextDisplayer _textDisplayer;
    public static TextDisplayer textDisplayer
    {
        get
        {
            if (!_textDisplayer)
            {
                _textDisplayer = GameObject.FindObjectOfType<TextDisplayer>();
            }

            return _textDisplayer;
        }
    }

    private static GameObject _player;
    public static GameObject player
    {
        get
        {
            if (!_player)
            {
                _player = GameObject.Find("Player");
            }

            return _player;
        }
    }

    private static GameObject _mom;
    public static GameObject mom
    {
        get
        {
            if (!_mom)
            {
                _mom = GameObject.Find("Mom");
            }

            return _mom;
        }
    }
}
