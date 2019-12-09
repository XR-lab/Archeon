using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinCondition : MonoBehaviour
{
    [SerializeField]
    private int _goal = 3;

    [SerializeField]
    private UnityEvent _onScoreAdded;

    [SerializeField]
    private UnityEvent _onGoalAccomplished;

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
            _onScoreAdded.Invoke();
            _currentAmount += 1;

            if (_currentAmount == _goal)
            {
                _onGoalAccomplished.Invoke();
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
