using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotMove : Properties
{
    public NavMeshAgent navMeshAgent;
    public Vector3 targetPos;

    private void Update()
    {
        if(!hasTarget && targetStacks.Count > 0)
        {
            ChooseTarget();
        }
        else if(!hasTarget && targetStacks.Count == 0)
        {
            if (animator.GetBool(StringCache.IsRunning))
            {
                navMeshAgent.SetDestination(transform.position);
                animator.SetBool(StringCache.IsRunning, false);
            }
        }
    }

    private void ChooseTarget()
    {
        hasTarget = true;
        targetPos = targetStacks[0].transform.position;
        navMeshAgent.SetDestination(targetPos);
        if (!animator.GetBool(StringCache.IsRunning))
        {
            animator.SetBool(StringCache.IsRunning, true);
        }
    }
}
