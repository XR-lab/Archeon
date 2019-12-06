using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueAction : MonoBehaviour
{
    public float delay = 0;

    public abstract IEnumerator Action(float delay);
}
