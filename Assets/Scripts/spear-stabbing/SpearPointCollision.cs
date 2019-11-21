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
        if (Mathf.Pow(2, other.gameObject.layer) == _terrainLayer.value && !_grabbed) {
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        if (Mathf.Pow(2, other.gameObject.layer) == _huntingLayer.value && !_hasFishOnTip) {
            CatchFish(other.gameObject.transform);
        }
    }

    public void OnPickUp() {
        _rb.constraints = RigidbodyConstraints.None;
    }

    void CatchFish(Transform fish) {
        fish.position = transform.position;
        fish.rotation = transform.rotation;
        fish.SetParent(transform);
    }
}
