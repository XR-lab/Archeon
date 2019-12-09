using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : DialogueAction
{
    public List<string> messages;

    private int _index = 0;

    public override IEnumerator Action(float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log(messages[_index]);

        _index++;
        _index %= messages.Count;

        Debug.Log(_index);
    }
}
