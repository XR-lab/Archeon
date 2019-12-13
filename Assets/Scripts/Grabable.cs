using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabable : MonoBehaviour
{
    [HideInInspector]
    public Grab _activeHand = null;
}
