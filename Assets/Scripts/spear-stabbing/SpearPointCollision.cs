using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearPointCollision : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private LayerMask _terrainLayer;
    [SerializeField] private LayerMask _huntingLayer;
    private bool _grabbed;
    public bool Grabbed { get { return _grabbed; } set { _grabbed = value; } }
    private bool _hasFishOnTip;

    void OnTriggerEnter(Collider other) {
        // Checks if layer mask contains layer
        if (_terrainLayer == (_terrainLayer | ( 1 << other.gameObject.layer)) && !_grabbed) {
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        if (_huntingLayer == (_huntingLayer | (1 << other.gameObject.layer)) && !_hasFishOnTip) {
            CatchFish(other.gameObject.transform);
        }
    }

    void CatchFish(Transform fish) {
        fish.position = transform.position;
        fish.rotation = transform.rotation;
        fish.SetParent(transform);
    }
}
