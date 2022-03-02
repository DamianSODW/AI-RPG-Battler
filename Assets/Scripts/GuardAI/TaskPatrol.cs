using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskPatrol : Node
{
    private Transform transform;
    private Animator animator;
    private Transform[] waypoints;

    private int currentWaypointIndex = 0;

    private float waitTime = 1f; // in seconds
    private float waitCounter = 0f;
    private bool waiting = false;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        this.transform = transform;
        this.waypoints = waypoints;
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                waiting = false;
                animator.SetBool("Walking", true);
            }
        }
        else
        {
            Transform currentWaypoint = waypoints[currentWaypointIndex];
            if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.01f)
            {
                transform.position = currentWaypoint.position;
                waitCounter = 0f;
                waiting = true;

                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                animator.SetBool("Walking", false);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, GuardBT.speed * Time.deltaTime);
                transform.LookAt(currentWaypoint.position);
            }
        }


        state = NodeState.Running;
        return state;
    }

}
