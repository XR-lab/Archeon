using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearGrab : MonoBehaviour {
    private Rigidbody _rb;
    private SpearPointCollision _point;

    void Start() {
        _rb = GetComponent<Rigidbody>();
        _point = GetComponentInChildren<SpearPointCollision>();
    }

    public void OnPickUp() {
        _point.Grabbed = true;
        _rb.constraints = RigidbodyConstraints.None;
    }

    public void OnDrop() {
        _point.Grabbed = false;
    }
}
