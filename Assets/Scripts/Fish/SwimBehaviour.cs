using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimBehaviour : StateMachineBehaviour
{
    private float _maxVelocity = 5;
    private float _angleBetween = 0.0f;

    private Vector3 _angle;
    private Vector3 _obstacle;
    private Vector3 _target;

    private GameObject _this;
    private FOV _fov;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _this = animator.transform.gameObject;
        _fov = _this.GetComponent<FOV>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _this.transform.position += (_this.transform.forward * _maxVelocity * Time.deltaTime);
        Transform[] _visibleTargets = _fov.visibleTargets.ToArray();

        if (_fov.visibleTargets.Count > 0)
        {
            var currentDistance = -1f;

            foreach (Transform _trans in _fov.visibleTargets)
            {
                float dist = Vector3.Distance(_this.transform.position, _trans.position);
                if (currentDistance == -1f || dist < currentDistance)
                {
                    currentDistance = dist;
                    _obstacle = _trans.position;
                }
            }

        Vector3 _newLookRotation = new Vector3(animator.transform.position.x - _obstacle.x,
                                               animator.transform.position.y,
                                               animator.transform.position.z - _obstacle.z);
            animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, Quaternion.LookRotation(_newLookRotation), 150 * Time.deltaTime);
        }

        Debug.DrawRay(animator.transform.position, new Vector3(animator.transform.position.x - _obstacle.x,
                                                               animator.transform.position.y,
                                                               animator.transform.position.z - _obstacle.z), 
                                                               Color.yellow);
    }



    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)

    {



    }
}