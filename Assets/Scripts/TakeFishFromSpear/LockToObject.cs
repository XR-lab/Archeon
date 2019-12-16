using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LockToObject : MonoBehaviour
{
    private Transform _trans;

    private Throwable _throwable;

    private Vector3 _positionOffset;
    private Quaternion _rotationOffset;

    private bool _registered;
    private bool _held;

    private Collider _col;
    private Collider[] _cols;

    private Rigidbody _rb;

    void Start() {
        _throwable = GetComponent<Throwable>();
        _col = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
        if (!_registered) {
            _throwable.onPickUp.AddListener(OnPickUp);
            _registered = true;
            _throwable.onDetachFromHand.AddListener(OnDrop);
        }
    }

    public void OnDrop() {
        _col.isTrigger = false;
        _held = false;
        _rb.constraints = RigidbodyConstraints.None;
        _rb.isKinematic = false;
        _rb.useGravity = true;
    }

    public void OnPickUp() {
        if (_trans != null) {
            _trans.GetComponentInChildren<SpearPointCollision>().HasFishOnTip = false;
            _trans = null;
        }
        _held = true;
        _col.isTrigger = true;
        _rb.velocity = Vector3.zero;
        if (_trans != null) {
            foreach (Collider c in _cols) {
                //Physics.IgnoreCollision(_col, c, false);
            }
        }
    }

    void Update() {
        if (_held || _trans == null) {
            return;
        }
        Vector3 targetPos = _trans.position;
        Quaternion targetRot = _trans.parent.rotation * _rotationOffset;
        transform.position = RotatePointAroundPivot(targetPos, _trans.position, targetRot);
        transform.rotation = targetRot;
    }

    public void SetFakeParent(Transform parent) {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("IsDead", true);
        anim.StopPlayback();
        anim.WriteDefaultValues();
        anim.enabled = false;
        _positionOffset = parent.position - transform.position;
        _rotationOffset = Quaternion.Inverse(parent.parent.rotation * transform.rotation);
        _trans = parent;
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation) {
        Vector3 dir = point - pivot;
        dir = rotation * dir;
        point = dir + pivot;
        return point;
    }
}
