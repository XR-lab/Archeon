using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearGrab : MonoBehaviour {
    private Rigidbody _rb;
    private SpearPointCollision[] _points;

    void Start() {
        _rb = GetComponent<Rigidbody>();
        _points = GetComponentsInChildren<SpearPointCollision>();
    }

    public void OnPickUp() {
        if (_points.Length > 0) {
            foreach (SpearPointCollision p in _points) {
                p.Grabbed = true;
            }
        }
        _rb.isKinematic = false;        
        GetComponent<AudioSource>().Play();
    }

    public void OnDrop() {
        if (_points.Length > 0) {
            foreach (SpearPointCollision p in _points) {
                p.Grabbed = false;
            }
        }
    }
}
