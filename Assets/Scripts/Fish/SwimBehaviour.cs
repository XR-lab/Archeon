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
        _target = animator.transform.forward * _maxVelocity;
        animator.transform.position += _target * Time.deltaTime;

        if (_fov.visibleTargets.Count > 0)
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

            }

            Vector3 _newLookRotation = new Vector3(animator.transform.position.x - _obstacle.x,
                                                   animator.transform.position.y,
                                                   animator.transform.position.z - _obstacle.z);
            animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, Quaternion.LookRotation(_newLookRotation), 200 * Time.deltaTime);
        }


        
        Debug.DrawRay(animator.transform.position, new Vector3(animator.transform.position.x - _obstacle.x,
                                                               animator.transform.position.y,
                                                               animator.transform.position.z - _obstacle.z), 
                                                               Color.yellow);





        //if (Input.GetKeyDown(KeyCode.Space))

        //{

        //    animator.SetBool("IsPanicing", true);

        //}



    }



    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)

    {



    }

    //private float _mass = 100;
    //private float _maxVelocity = 2;
    //private float _maxForce = 15;

    //private Vector3 _velocity;
    //private GameObject _target;
    //private Transform[] _targets;
    //private Transform _destination;
    //private Transform _oldDest;
    //private int _destNum = 0;


    //public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    //{
    //    _target = GameObject.Find("Targets");
    //    _targets = _target.GetComponentsInChildren<Transform>();
    //    _velocity = Vector3.zero;
    //    GetDestination();
    //}

    //public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    //{
    //    Vector3 _desiredVelocity = _destination.transform.position - animator.transform.position;
    //    _desiredVelocity = _desiredVelocity.normalized * _maxVelocity;

    //    Vector3 _steering = _desiredVelocity - _velocity;
    //    _steering = Vector3.ClampMagnitude(_steering, _maxForce);
    //    _steering /= _mass;

    //    _velocity = Vector3.ClampMagnitude(_velocity + _steering, _maxVelocity);
    //    animator.transform.position += _velocity * Time.deltaTime;
    //    animator.transform.forward = _velocity.normalized;

    //    Debug.DrawRay(animator.transform.position, _velocity.normalized * 2, Color.green);
    //    Debug.DrawRay(animator.transform.position, _desiredVelocity.normalized * 2, Color.magenta);

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        animator.SetBool("IsPanicing", true);
    //    }

    //    //animator.transform.position = Vector3.MoveTowards(animator.transform.position, _destination.position, _maxVelocity * Time.deltaTime);

    //    if (Input.GetKeyDown(KeyCode.X))
    //    {
    //        animator.SetBool("IsPanicing", true);
    //    }

    //    if (Vector3.Distance(animator.transform.position, _destination.transform.position) <= 0.5)
    //    {
    //        _oldDest = _destination;
    //        GetDestination();
    //    }

    //    if (_destination == _oldDest)
    //    {
    //        GetDestination();
    //    }

    //    //Debug.DrawLine(animator.transform.position, _destination.transform.position, Color.green);

    //}

    //public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    //{

    //}

    //private void GetDestination()
    //{
    //    _destNum = Mathf.Clamp(Mathf.RoundToInt(Random.Range(0, _targets.Length)), 0, _targets.Length);
    //    _destination = _targets[_destNum];

    //    Debug.Log("DestNum in array = " + _destNum);
    //    Debug.Log("Destination in array = " + _destination);

    //}
}