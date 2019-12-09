using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SpearPointCollision : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private LayerMask _terrainLayer;
    [SerializeField] private LayerMask _huntingLayer;
    [SerializeField] private Transform _fishHolder;
    //[SerializeField] private SteamVR_Skeleton_Pose _fishPose;
    private bool _grabbed;
    public bool Grabbed { get { return _grabbed; } set { _grabbed = value; } }
    private bool _hasFishOnTip;
    public bool HasFishOnTip { get { return _hasFishOnTip; } set { _hasFishOnTip = value; } }

    void OnTriggerEnter(Collider other) {
        // Checks if layer mask contains layer
        if (_terrainLayer == (_terrainLayer | ( 1 << other.gameObject.layer)) && !_grabbed) {
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        if (_huntingLayer == (_huntingLayer | (1 << other.gameObject.layer)) && !_hasFishOnTip) {
            _hasFishOnTip = true;
            CatchFish(other.gameObject.transform);
        }
    }

    void CatchFish(Transform fish) {
        Debug.LogError("Stabbing the fish: " + fish.name);
        fish.GetComponent<Animator>().enabled = false;
        fish.rotation = _fishHolder.rotation;
        fish.position = _fishHolder.position;
        Rigidbody rb = fish.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        LockToObject _lock = fish.gameObject.GetComponent<LockToObject>();
        fish.GetComponent<DisableCollision>().ToggleCollision();
        fish.GetComponent<RagdollHandler>().RagdollActiveTo(true);
        if (_lock == null) {
            _lock = fish.gameObject.AddComponent<LockToObject>();
        }
        _lock.enabled = true;
        _lock.SetFakeParent(transform);
    }
}
