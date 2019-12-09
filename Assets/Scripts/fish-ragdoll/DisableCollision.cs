using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollision : MonoBehaviour
{
    [SerializeField]
    private Collider _collider;

    public void ToggleCollision()
    {
        _collider.enabled = !_collider.enabled;
    }
}
