using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearPointCollision : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private LayerMask _terrainLayer;
    [SerializeField] private LayerMask _huntingLayer;
    [SerializeField] private Transform _fishHolder;
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
        Debug.LogError("Stabbing the fish: " + fish.name);
        fish.GetComponent<Animator>().enabled = false;
        fish.position = _fishHolder.position;
        fish.rotation = _fishHolder.rotation;
        fish.SetParent(_fishHolder);
    }
}
