using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LockToObject : MonoBehaviour
{
    public Transform Trans { get; set; }

    private Transform _trans;

    private LockToObject _instance;

    private Throwable _throwable;

    private Vector3 _positionOffset;
    private Quaternion _rotationOffset;

    private void Start() {
        _instance = this;
        _throwable = GetComponent<Throwable>();
        _throwable.onPickUp.AddListener(DisableSelf);
    }

    public void DisableSelf() {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Physics.IgnoreCollision(GetComponent<Collider>(), /*getting the collider of the spear itself*/_trans.parent.parent.parent.GetComponent<Collider>(), false);
        _throwable.onPickUp.RemoveListener(DisableSelf);
        _instance.enabled = false;
    }

    void Update()
    {
        Vector3 targetPos = _trans.position - _positionOffset;
        Quaternion targetRot = _trans.rotation * _rotationOffset;

        transform.position = RotatePointAroundPivot(targetPos, _trans.position, targetRot);
        transform.rotation = targetRot;
    }

    public void SetFakeParent(Transform parent) {
        _positionOffset = parent.position - transform.position;
        _rotationOffset = Quaternion.Inverse(parent.rotation * transform.rotation);
        _trans = parent;
        Physics.IgnoreCollision(GetComponent<Collider>(), _trans.parent.parent.parent.GetComponent<Collider>());
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation) {
        Vector3 dir = point - pivot;
        dir = rotation * dir;
        point = dir + pivot;
        return point;
    }
}
