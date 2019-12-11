﻿using System.Collections;
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
            _rb.isKinematic = true;
        }
        if (_huntingLayer == (_huntingLayer | (1 << other.gameObject.layer)) && !_hasFishOnTip) {
            _hasFishOnTip = true;
            CatchFish(other.gameObject.transform);
        }
    }

    void CatchFish(Transform fish) {
        Debug.LogError("Stabbing the fish: " + fish.name);
        fish.GetComponentInParent<Animator>().SetBool("IsDead", true);
        fish.rotation = _fishHolder.rotation;
        fish.position = _fishHolder.position;
        LockToObject _lock = fish.gameObject.GetComponentInParent<LockToObject>();
        _lock.enabled = true;
        _lock.SetFakeParent(transform);
    }
}
