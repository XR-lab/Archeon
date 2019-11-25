using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearVelocityToRotation : MonoBehaviour {

    private Rigidbody _rb;
    [SerializeField] private float _rotationSpeed = 1f, _maxAngle = 0.0f;
    [SerializeField] private bool _canRotate;
    [SerializeField] private Transform _forwardDirection;

    private Vector3 _targetDirection;
    private Vector3 _newDirection;

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (!_canRotate) {
            return;
        }
        _rb.angularVelocity = Vector3.zero;
        _newDirection = Vector3.Slerp(transform.rotation.eulerAngles, Vector3.zero, _rotationSpeed);
        transform.rotation = Quaternion.Euler(_newDirection);
    }

    public void OnDetatchFromHand() {
        _canRotate = true;
    }

    public void StopRotating() {
        _canRotate = false;
    }

    void OnCollisionEnter(Collision collision) {
        StopRotating();
    }

}
