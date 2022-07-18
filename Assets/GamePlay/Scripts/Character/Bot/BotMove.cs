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
        if (!hasTarget && targetStacks.Count > 0)
        {
            ClaimStack();
            if (!animator.GetBool(StringCache.IsRunning))
            {
                animator.SetBool(StringCache.IsRunning, true);
            }
        }
        else if (!hasTarget && targetStacks.Count == 0)
        {
            StartCoroutine(BuildBridge());
        }
    }

    public IEnumerator BuildBridge()
    {
        navMeshAgent.SetDestination(transform.position);
        yield return new WaitForSeconds(0.3f);
        if (targetStacks.Count == 0)
        {
            hasTarget = true;
            targetPos = targetBridge.transform.position;
            navMeshAgent.SetDestination(targetPos);
        }
    }

    private void ClaimStack()
    {
        hasTarget = true;
        targetPos = targetStacks[0].transform.position;
        navMeshAgent.SetDestination(targetPos);
    }
}