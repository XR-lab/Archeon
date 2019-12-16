using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearThrowRotation : MonoBehaviour {

    private Rigidbody _rb;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private bool _canRotate;

    private Vector3 _deltaPosition;
    private Vector3 _newPosition;
    private Vector3 _oldPosition;
    private Quaternion _newRotation;
    private Quaternion _oldRotation;

    void Start() {
        _rb = GetComponent<Rigidbody>();
        _oldPosition = transform.position;    
    }

    void Update() {
        if (!_canRotate) {
            return;
        }
        _rb.angularVelocity = Vector3.zero;
        _newPosition = transform.position;
        if (_newPosition != _oldPosition) {
            _deltaPosition = _newPosition - _oldPosition;
            _oldRotation = transform.rotation;
            transform.LookAt(transform.position + _deltaPosition);
            _newRotation = transform.rotation;
            transform.rotation = _oldRotation;
            transform.rotation = Quaternion.RotateTowards(_oldRotation, _newRotation, _rotationSpeed);
        }
        _oldPosition = _newPosition;
    }

    public void OnDetatchFromHand() {
        _oldPosition = transform.position;
        _canRotate = true;
    }

    public void StopRotating() {
        _canRotate = false;
    }

    void OnCollisionEnter(Collision collision) {
        StopRotating();
    }

}
