using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber_attack01 : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsDead", true);
    }
}