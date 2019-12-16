using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Base class of the dialogue
public abstract class DialogueAction : MonoBehaviour
{
    public List<float> delays;

    public abstract IEnumerator Action(string index, Action callback);
}
