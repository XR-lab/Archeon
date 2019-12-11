using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{

    private Rigidbody[] _rbs;
    private Collider[] _cols;

    [SerializeField] private Transform _trans;

    void Awake()
    {
        _rbs = GetComponentsInChildren<Rigidbody>();
        _cols = GetComponentsInChildren<Collider>();
    }

    public void EnableRagdoll() {
        foreach (Rigidbody rb in _rbs) {
            rb.isKinematic = false;
        }
        foreach (Collider c in _cols) {
            c.enabled = true;
        }
        GetComponent<Collider>().enabled = false;
        _trans.GetComponent<Rigidbody>().isKinematic = true;
    }
}
