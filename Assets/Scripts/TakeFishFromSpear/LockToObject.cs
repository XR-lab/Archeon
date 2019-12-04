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

    private bool _registered;
    private bool _held;

    private void Start() {
        _instance = this;
        _throwable = GetComponentInChildren<Throwable>();
        if (!_registered) {
            _throwable.onPickUp.AddListener(OnPickUp);
            _registered = true;
            _throwable.onDetachFromHand.AddListener(OnDrop);
        }
    }

    public void OnDrop() {
        _held = false;
    }

    public void OnPickUp() {
        if (_trans != null) {
            _trans.GetComponent<SpearPointCollision>().HasFishOnTip = false;
            _trans = null;
        }
        _held = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (_trans != null) {
            Physics.IgnoreCollision(GetComponent<Collider>(), /*getting the collider of the spear itself*/_trans.parent.parent.parent.GetComponent<Collider>(), false);
        }
    }

    void Update() {
        if (_held || _trans == null) {
            return;
        }
        Vector3 targetPos = _trans.position - _positionOffset;
        Quaternion targetRot = _trans.rotation * _rotationOffset;

        transform.position = RotatePointAroundPivot(targetPos, _trans.position, targetRot);
        transform.rotation = targetRot;
    }

    public void SetFakeParent(Transform parent) {
        GetComponent<RagdollHandler>().RagdollActiveTo(true);
        Animator anim = GetComponent<Animator>();
        anim.SetBool("IsDead", true);
        anim.enabled = false;
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
