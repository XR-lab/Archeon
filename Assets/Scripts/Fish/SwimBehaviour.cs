using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimBehaviour : StateMachineBehaviour
{
    private float _maxVelocity = 0.5f;
    private float _startY;
    private Vector3 _obstacle;
    private GameObject _this;
    private FOV _fov;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _this = animator.transform.gameObject;
        _fov = _this.GetComponent<FOV>();

        _startY = _this.transform.position.y;
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

                if (_trans.root.name == "Spear")
                {
                    animator.SetBool("IsPanicing", true);
                }
            }

        Vector3 _newLookRotation = new Vector3(_this.transform.position.x - _obstacle.x,
                                               _this.transform.position.y,
                                               _this.transform.position.z - _obstacle.z);
            _this.transform.rotation = Quaternion.RotateTowards(_this.transform.rotation, Quaternion.LookRotation(_newLookRotation), 150 * Time.deltaTime);
        }

        Debug.DrawRay(_this.transform.position, new Vector3(_this.transform.position.x - _obstacle.x,
                                                               _this.transform.position.y,
                                                               _this.transform.position.z - _obstacle.z), 
                                                               Color.yellow);
        
        _this.transform.position = new Vector3(_this.transform.position.x,
                                               _startY,
                                               _this.transform.position.z);
    }



    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)

    {



    }
}