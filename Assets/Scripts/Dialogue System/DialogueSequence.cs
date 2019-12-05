using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequence : MonoBehaviour
{
    [SerializeField]
    private string _id;

    public string Id
    {
        get { return _id; }
        private set { _id = Id; }
    }

    [SerializeField]
    private List<DialogueAction> _actionSequence;

    public IEnumerator ActivateSequence()
    {
        foreach (DialogueAction action in _actionSequence)
        {
            yield return new WaitForEndOfFrame();

            StartCoroutine(action.Action(action.delay));
        }
    }
}
