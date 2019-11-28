using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private int _goal = 3;
    private int _currentAmount;

    void Start()
    {
        _currentAmount = 0; 
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.gameObject.layer == 10)
        {
            Debug.Log("Stored Fish");

            _currentAmount += 1;

            if (_currentAmount == _goal)
            {
                Debug.Log("U won");
            }
        }
    }

    private void OnCollisionExit(Collision col)
    {

        if (col.transform.gameObject.layer == 10)
        {
            Debug.Log("Released fish");
            _currentAmount -= 1;
        }
    }
}
