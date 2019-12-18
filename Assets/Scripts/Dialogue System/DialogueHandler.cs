using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField]
    private List<DialogueSequence> _actions;

    public SteamVR_Action_Boolean headsetOnHead = SteamVR_Input.GetBooleanAction("HeadsetOnHead");


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

    private void Update() {

        if (headsetOnHead.GetStateDown(SteamVR_Input_Sources.Head)) 
        {
            Invoke("StartSequence", 2);
        }
    }

    private void StartSequence() 
    {
        ActivateSequence("INTRO");
    }

    public void EndSequence() {
        ActivateSequence("WIN");
    }

    
}
