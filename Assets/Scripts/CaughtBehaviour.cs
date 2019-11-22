using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtBehaviour : StateMachineBehaviour {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        animator.StopPlayback();
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);
    }
}
