using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicBehaviour : StateMachineBehaviour
{
    private GameObject target;
    private Transform[] _targets;
    private Transform _impactPos;
    private Transform _destination;
    private Transform _closestTarget;
    private float _impactAngle;
    private float _speed = 4;
    private float _closestDistance = 1000f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        target = GameObject.Find("Targets");
        _targets = target.GetComponentsInChildren<Transform>();

        _impactPos = GameObject.FindWithTag("SpearTip").transform;

        foreach (Transform trans in _targets)
        {
            var distanceFromTarget = Vector3.Distance(animator.transform.position, trans.position);
            if (distanceFromTarget < _closestDistance)
            {
                _closestTarget = trans;
                _closestDistance = distanceFromTarget;
            }
        }

        _destination = _closestTarget;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, _destination.position ,_speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("IsPanicing", false);
        }

        Debug.DrawLine(animator.transform.position, _destination.transform.position, Color.red);

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }
}
