using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimBehaviour : StateMachineBehaviour
{
    private float _mass = 100;
    private float _maxVelocity = 2;
    private float _maxForce = 15;

    private Vector3 _velocity;
    private GameObject _target;
    private Transform[] _targets;
    private Transform _destination;
    private Transform _oldDest;
    private int _destNum = 0;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _target = GameObject.Find("Targets");
        _targets = _target.GetComponentsInChildren<Transform>();
        _velocity = Vector3.zero;
        GetDestination();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsPanicing", true);
        }

        //animator.transform.position = Vector3.MoveTowards(animator.transform.position, _destination.position, _maxVelocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetBool("IsPanicing", true);
        }

        if (Vector3.Distance(animator.transform.position, _destination.transform.position) <= 0.5)
        {
            _oldDest = _destination;
            GetDestination();
        }

        if (_destination == _oldDest)
        {
            GetDestination();
        }

        //Debug.DrawLine(animator.transform.position, _destination.transform.position, Color.green);

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    private void GetDestination()
    {
        _destNum = Mathf.Clamp(Mathf.RoundToInt(Random.Range(0, _targets.Length)), 0, _targets.Length);
        _destination = _targets[_destNum];

        Debug.Log("DestNum in array = " + _destNum);
        Debug.Log("Destination in array = " + _destination);

    }
}