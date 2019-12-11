using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LockToObject : MonoBehaviour
{
    [SerializeField] private Transform _lockTransform;

    public Transform Trans { get; set; }

    private Transform _trans;

    private Throwable _throwable;

    private Vector3 _positionOffset;
    private Quaternion _rotationOffset;

    private bool _registered;
    private bool _held;

    void OnEnable() {
        _throwable = GetComponentInChildren<Throwable>();
        if (!_registered) {
            _throwable.onPickUp.AddListener(OnPickUp);
            _registered = true;
            _throwable.onDetachFromHand.AddListener(OnDrop);
        }
        GetComponent<RagdollController>().EnableRagdoll();
    }

    public void OnDrop() {
        _held = false;
    }

    public void OnPickUp() {
        if (_trans != null) {
            _trans.GetComponent<SpearPointCollision>().HasFishOnTip = false;
            _trans = null;
        }
        _lockTransform.GetComponent<Rigidbody>().isKinematic = false;
        _held = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void Update() {
        if (_held || _trans == null) {
            return;
        }
        Vector3 targetPos = _trans.position - _positionOffset;
        Quaternion targetRot = _trans.rotation * _rotationOffset;
        _lockTransform.position = RotatePointAroundPivot(targetPos, _trans.position, targetRot);
        _lockTransform.rotation = targetRot;
    }

    public void SetFakeParent(Transform parent) {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("IsDead", true);
        anim.enabled = false;
        _positionOffset = parent.position - transform.position;
        _rotationOffset = Quaternion.Inverse(parent.rotation * transform.rotation);
        _trans = parent;
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation) {
        Vector3 dir = point - pivot;
        dir = rotation * dir;
        point = dir + pivot;
        return point;
    }
}
