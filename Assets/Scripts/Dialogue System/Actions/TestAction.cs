using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TestAction : DialogueAction
{
    [SerializeField]
    private List<string> _messageSpecifier;

    [SerializeField]
    private List<string> _messages;

    public override IEnumerator Action(string index, Action callback)
    {
        yield return new WaitForSeconds(delays[_messageSpecifier.FindIndex((string match) => index == match)]);

        Debug.Log(_messages[_messageSpecifier.FindIndex((string match) => index == match)]);
        callback?.Invoke();
    }
}
