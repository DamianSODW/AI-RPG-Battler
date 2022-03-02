using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInFOVRange : Node
{
    private static int enemyLayerMask = 1 << 6;

    private Transform transform;
    private Animator animator;

    public CheckEnemyInFOVRange(Transform transform)
    {
        this.transform = transform;
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");
        if (target == null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                transform.position, GuardBT.fovRange, enemyLayerMask);

            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                animator.SetBool("Walking", true);
                return NodeState.Success;
            }
            return NodeState.Failure;
        }
        return NodeState.Success;
    }

}
