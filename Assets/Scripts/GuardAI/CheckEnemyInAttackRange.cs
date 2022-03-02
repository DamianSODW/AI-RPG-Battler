using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    private Transform transform;
    private Animator animator;


    public CheckEnemyInAttackRange(Transform transform)
    {
        this.transform = transform;
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");
        if (target == null)
        {
            return NodeState.Failure;
        }

        Transform targetTransform = (Transform)target;
        if (Vector3.Distance(transform.position, targetTransform.position) <= GuardBT.attackRange)
        {
            animator.SetBool("Attacking", true);
            animator.SetBool("Walking", false);

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}
