using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimBehaviour : StateMachineBehaviour
{
    //private float _mass = 100;
    //private float _maxVelocity = 2;
    //private float _maxForce = 15;
    private float _angleBetween = 0.0f;
    private float _angle;


    //private Vector3 _velocity;
    //private Vector3 _destination;
    private Vector3 _target;


    private GameObject _this;
    public FOV _fov;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _this = animator.transform.gameObject;
        _fov = _this.GetComponent<FOV>();
        
        //_velocity = Vector3.zero;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Transform[] _visibleTargets = _this.GetComponent<FOV>().visibleTargets.ToArray();

        _target = animator.transform.forward * 2;

        Vector3 targetDir = _target - animator.transform.position;
        _angleBetween = Vector3.Angle(animator.transform.forward, targetDir);

        
        animator.transform.position += _target * Time.deltaTime;

        if (_fov.visibleTargets.Count >= 0)
        {
            Debug.Log("Avoiding");
            _angle = Vector3.Angle(animator.transform.position, _visibleTargets[0].transform.position);
            animator.transform.rotation = Vector3.ClampMagnitude(Quaternion.Euler(0, -_angle, 0), 2);
        }

        else
        {
            _angle = animator.transform.rotation.y;
        }

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
        //Debug.DrawRay(animator.transform.position, _desiredVelocity.normalized * 5, Color.magenta);
        //Debug.DrawRay(animator.transform.position, animator.transform.forward * 10, Color.yellow);


        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    animator.SetBool("IsPanicing", true);
        //}

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }        
}