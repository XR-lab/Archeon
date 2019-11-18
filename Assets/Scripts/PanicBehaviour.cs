using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicBehaviour : StateMachineBehaviour
{
    private float _mass = 80;
    private float _maxVelocity = 6;
    private float _maxForce = 20;
    private float _panicAmount = 0;
    private float _closestDistance = 1000f;

    private Vector3 _velocity;
    private GameObject target;
    private Transform[] _targets;
    private Transform _impactPos;
    private Transform _destination;
    private Transform _closestTarget;
    private int _destNum = 0;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        target = GameObject.Find("Targets");
        _targets = target.GetComponentsInChildren<Transform>();

        _impactPos = GameObject.FindWithTag("SpearTip").transform;

        GetDestination(animator);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Vector3 _desiredVelocity = _destination.transform.position - animator.transform.position;
        _desiredVelocity = _desiredVelocity.normalized * _maxVelocity;

        Vector3 _steering = _desiredVelocity - _velocity;
        _steering = Vector3.ClampMagnitude(_steering, _maxForce);
        _steering /= _mass;

        _velocity = Vector3.ClampMagnitude(_velocity + _steering, _maxVelocity);
        animator.transform.position += _velocity * Time.deltaTime;
        animator.transform.forward = _velocity.normalized;

        Debug.DrawRay(animator.transform.position, _velocity.normalized * 2, Color.green);
        Debug.DrawRay(animator.transform.position, _desiredVelocity.normalized * 2, Color.magenta);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("IsPanicing", false);
        }

        Debug.DrawLine(animator.transform.position, _destination.transform.position, Color.red);

        if (Vector3.Distance(animator.transform.position, _destination.transform.position) <= 0.5)
        {
            GetDestination(animator);

            _panicAmount++;

            if (_panicAmount == 2)
            {
                animator.SetBool("IsPanicing", false);
                _panicAmount = 0;
            }
        }   
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    private void GetDestination(Animator animator)
    {
        _destNum = Mathf.Clamp(Mathf.RoundToInt(UnityEngine.Random.Range(0, _targets.Length)), 0, _targets.Length);
        _destination = _targets[_destNum];
    }
}
