using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimBehaviour : StateMachineBehaviour
{
    //private float _mass = 100;
    private float _maxVelocity = 3;
    //private float _maxForce = 15;
    private float _angleBetween = 0.0f;
    private Vector3 _angle;



    //private Vector3 _velocity;
    private Vector3 _destination;
    private Vector3 _target;

    private GameObject _this;
    private FOV _fov;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _this = animator.transform.gameObject;
        _fov = _this.GetComponent<FOV>();

        //_velocity = Vector3.zero;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Transform[] _visibleTargets = _this.GetComponent<FOV>().visibleTargets.ToArray();
        _target = animator.transform.forward * _maxVelocity;

        Vector3 _targetDir = _target - animator.transform.position;
        _angleBetween = Vector3.Angle(animator.transform.forward, _targetDir);

        animator.transform.position += _target * Time.deltaTime;

        if (_fov.visibleTargets.Count > 0)
        {
            var currentDistance = -1f;
            foreach (Transform _trans in _fov.visibleTargets)
            {
                float dist = Vector3.Distance(animator.transform.position, _trans.position);
                if(currentDistance == -1f || dist < currentDistance)
                {
                    currentDistance = dist;
                    _destination = _trans.position;
                }
            }

            Debug.Log(_fov.visibleTargets.Count);
            Debug.Log("Destination: " + _destination);
            //float _angleDegrees = Vector3.Angle(animator.transform.position, _visibleTargets[0].transform.position);
            float _angleDegrees = Vector3.Angle(animator.transform.position, _destination);
            _angle = new Vector3(0, _angleDegrees, 0);

            Debug.Log("_angleDegrees: " + _angleDegrees);
            Debug.Log("_angle: " + _angle);

            //animator.transform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, -_angle, 0), 1f);
            animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, Quaternion.Euler(0f, _this.gameObject.transform.rotation.y - _angleDegrees, 0f), 1f);
            Debug.Log("animator.transform.rotation: " + animator.transform.rotation);
        }

        //Vector3 _desiredVelocity = _targetDir - animator.transform.position;
        //_desiredVelocity = _desiredVelocity.normalized * _maxVelocity;

        //Vector3 _steering = _desiredVelocity - _velocity;
        //_steering = Vector3.ClampMagnitude(_steering, _maxForce);
        //_steering /= _mass;

        //_velocity = Vector3.ClampMagnitude(_velocity + _steering, _maxVelocity);
        //animator.transform.position += _velocity * Time.deltaTime;
        //animator.transform.forward = _velocity.normalized;

        //Debug.DrawRay(animator.transform.position, _desiredVelocity.normalized * 5, Color.magenta);

        //else
        //{
        //    _angle = animator.transform.rotation.y;
        //}

        //animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, Quaternion.LookRotation(animator.transform.position - _destination), 5);
        //animator.transform.position = animator.transform.forward * 5 * Time.deltaTime;


        //Vector3 _desiredVelocity = _destination - animator.transform.position;
        //_desiredVelocity = _desiredVelocity.normalized * _maxVelocity;

        //Vector3 _steering = _desiredVelocity - _velocity;
        //_steering = Vector3.ClampMagnitude(_steering, _maxForce);
        //_steering /= _mass;

        //_velocity = Vector3.ClampMagnitude(_velocity + _steering, _maxVelocity);
        //animator.transform.position += _velocity * Time.deltaTime;
        //animator.transform.forward = _velocity.normalized;

        //Debug.DrawRay(animator.transform.position, _velocity.normalized * 5, Color.green);
        //Debug.DrawRay(animator.transform.position, animator.transform.forward * 10, Color.yellow);

        Debug.DrawRay(animator.transform.position, _destination, Color.yellow);


        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    animator.SetBool("IsPanicing", true);
        //}

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }
}