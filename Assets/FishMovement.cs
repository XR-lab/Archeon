using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private float _maxVelocity = 5;
    private float _angleBetween = 0.0f;

    private Vector3 _angle;
    private Vector3 _obstacle;
    private Vector3 _target;

    private FOV _fov;

    void Start()
    {
        _fov = GetComponent<FOV>();
    }

    void Update()
    {
        transform.position += (transform.forward * _maxVelocity * Time.deltaTime);
        Transform[] _visibleTargets = _fov.visibleTargets.ToArray();

        
        if (_fov.visibleTargets.Count > 0)
        {
            var currentDistance = -1f;

            foreach (Transform _trans in _fov.visibleTargets)
            {
                float dist = Vector3.Distance(transform.position, _trans.position);
                if (currentDistance == -1f || dist < currentDistance)
                {
                    currentDistance = dist;
                    _obstacle = _trans.position;
                }
            }

        }
    }
}
