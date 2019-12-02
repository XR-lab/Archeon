using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private int _goal = 3;
    private int _currentAmount;

    [SerializeField] private LayerMask _acceptedLayers;

    void Start()
    {
        _currentAmount = 0; 
    }

    private void OnTriggerEnter(Collider col)
    {
        if (_acceptedLayers == (_acceptedLayers | (1 << col.gameObject.layer)))
        {
            Debug.Log("Stored Fish");

            _currentAmount += 1;

            if (_currentAmount == _goal)
            {
                Debug.Log("U won");
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {

        if (_acceptedLayers == (_acceptedLayers | (1 << col.gameObject.layer)))
        {
            Debug.Log("Released fish");
            _currentAmount -= 1;
        }
    }
}
