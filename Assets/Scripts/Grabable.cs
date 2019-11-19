using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Grabable : MonoBehaviour
{
    [HideInInspector]
    public Grab _activeHand = null;

    public UnityEvent OnPickup;
    public UnityEvent OnDrop;
}
