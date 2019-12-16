using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtBehaviour : StateMachineBehaviour {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        animator.StopPlayback();
        Rigidbody rb = animator.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);
    }
}
