using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpearGrab : MonoBehaviour {
    private Rigidbody _rb;
    private SpearPointCollision _point;
    private AudioSource _audioData;

    void Start() {
        _rb = GetComponent<Rigidbody>();
        _point = GetComponentInChildren<SpearPointCollision>();
        _audioData = GetComponent<AudioSource>();
    }

    public void OnPickUp() {
        _point.Grabbed = true;
        _rb.constraints = RigidbodyConstraints.None;
        _audioData.Play(0);
        
    }

    public void OnDrop() {
        _point.Grabbed = false;
    }
}
