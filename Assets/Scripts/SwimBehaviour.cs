using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimBehaviour : StateMachineBehaviour
{
    private GameObject target;
    private Transform[] _targets;
    private Transform _destination;
    private Transform _oldDest;
    private float _speed = 2;
    private int _destNum = 0;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        target = GameObject.Find("Targets");
        _targets = target.GetComponentsInChildren<Transform>();

        GetDestination();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsPanicing", true);
        }

        animator.transform.position = Vector3.MoveTowards(animator.transform.position, _destination.position, _speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsPanicing", true);
        }

        if (animator.transform.position == _destination.transform.position)
        {
            _oldDest = _destination;
            GetDestination();
        }

        if (_destination == _oldDest)
        {
            GetDestination();
        }

        Debug.DrawLine(animator.transform.position, _destination.transform.position);

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
