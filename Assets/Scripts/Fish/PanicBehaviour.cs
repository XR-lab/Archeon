using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicBehaviour : StateMachineBehaviour
{
    private float _maxVelocity = 1;
    private float _startY;
    private Vector3 _panicPoint;
    private GameObject _this;
    private FOV _fov;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _this = animator.transform.gameObject;
        _fov = _this.GetComponent<FOV>();
        _startY = _this.transform.position.y;

        foreach (Transform _trans in _fov.visibleTargets)
        {
            if (_trans.root.name == "Spear")
            {
                _panicPoint = _trans.root.transform.position;
            }
        }

        Vector3 _newLookRotation = new Vector3(animator.transform.position.x - _panicPoint.x,
                                                   animator.transform.position.y,
                                                   animator.transform.position.z - _panicPoint.z);
        animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, Quaternion.LookRotation(_newLookRotation), 200 * Time.deltaTime);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (Vector3.Distance(_panicPoint, _this.transform.position) > 12)
        {
            animator.SetBool("IsPanicing", false);
        }

        _this.transform.position += (_this.transform.forward * _maxVelocity * Time.deltaTime);
        Transform[] _visibleTargets = _fov.visibleTargets.ToArray();
        _this.transform.position = new Vector3(_this.transform.position.x,
                                               _startY,
                                               _this.transform.position.z);

        //if (_fov.visibleTargets.Count > 0)
        //{
        //    var currentDistance = -1f;

        //    foreach (Transform _trans in _fov.visibleTargets)
        //    {
        //        float dist = Vector3.Distance(_this.transform.position, _trans.position);
        //        if (currentDistance == -1f || dist < currentDistance)
        //        {
        //            currentDistance = dist;
        //            _obstacle = _trans.position;
        //        }
        //    }

        //    Vector3 _newLookRotation = new Vector3(animator.transform.position.x - _obstacle.x,
        //                                           animator.transform.position.y,
        //                                           animator.transform.position.z - _obstacle.z);
        //    animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, Quaternion.LookRotation(_newLookRotation), 200 * Time.deltaTime);
        //}

        //Debug.DrawRay(animator.transform.position, new Vector3(animator.transform.position.x - _obstacle.x,
        //                                                       animator.transform.position.y,
        //                                                       animator.transform.position.z - _obstacle.z),
        //                                                       Color.yellow);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }
}