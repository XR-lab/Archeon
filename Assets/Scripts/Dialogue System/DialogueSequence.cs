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
    private List<string> _actionSpecifiers;

    [SerializeField]
    private List<DialogueAction> _actionSequence;

    private bool nextAction = false;

    private void next()
    {
        nextAction = true;
    }

    public IEnumerator ActivateSequence()
    {
        Debug.Log("Activate sequence " + _id);
        for(int i = 0; i < _actionSequence.Count; i++)
        {
            DialogueAction action = _actionSequence[i];
            StartCoroutine(action.Action(_actionSpecifiers[i],next));
            yield return new WaitUntil(() => nextAction);
            nextAction = false;
            Debug.Log(i);
        }
        Debug.Log("Sequence " + Id + " finished");
    }
}
