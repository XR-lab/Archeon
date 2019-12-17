using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField]
    private List<DialogueSequence> _actions;

    public void ActivateSequence(string id)
    {
        foreach (DialogueSequence sequence in _actions)
        {
            if (sequence.Id == id)
            {
                StartCoroutine(sequence.ActivateSequence());
                break;
            }
        }
        
    }
}
