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
    public Transform FishHolder { get { return _fishHolder; } set { _fishHolder = value; } }
    [SerializeField] private bool _canStabFish;
    private bool _grabbed;
    public bool Grabbed { get { return _grabbed; } set { _grabbed = value; } }
    private bool _hasFishOnTip;
    public bool HasFishOnTip { get { return _hasFishOnTip; } set { _hasFishOnTip = value; } }

    void OnTriggerEnter(Collider other) {
        // Checks if layer mask contains layer
        if (_terrainLayer == (_terrainLayer | ( 1 << other.gameObject.layer)) && !_grabbed) {
            _rb.isKinematic = true;
        }
        if (_huntingLayer == (_huntingLayer | (1 << other.gameObject.layer)) && !_hasFishOnTip && _canStabFish) {
            _hasFishOnTip = true;
            CatchFish(other.gameObject.transform);
        }
    }

    public void StickToPek(Transform trans, SpearPointCollision previousPoint) {
        Destroy(transform.parent.GetComponent<Throwable>());
        Destroy(transform.parent.GetComponent<Valve.VR.InteractionSystem.Interactable>());
        _rb = transform.root.GetComponent<Rigidbody>();
        _fishHolder = trans;
        GetComponent<Collider>().isTrigger = true;
        Destroy(previousPoint);
    }

    void CatchFish(Transform fish) {
        Debug.LogError("Stabbing the fish: " + fish.name);
        fish.GetComponent<Animator>()?.SetBool("IsDead", true);
        fish.rotation = transform.root.rotation;
        fish.position = transform.root.position;
        LockToObject _lock = fish.gameObject.GetComponent<LockToObject>();
        _lock.enabled = true;
        _lock?.SetFakeParent(transform.parent);
    }
}
