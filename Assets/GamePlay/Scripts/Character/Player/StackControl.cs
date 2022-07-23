using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackControl : MonoBehaviour
{
    public Properties properties;
    public GameObject bag;
    public GameObject prevStack;
    public float stackFallRadius;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(StringCache.Stack) && other.GetComponent<Renderer>().material.color.Equals(properties.color)
            && properties.characterState == CharacterState.ClaimStack)
        {
            AddStack(other.gameObject);
        }

        if (other.CompareTag(StringCache.Player))
        {
            //if (properties.characterState == CharacterState.ClaimStack)
            //{
            //    if (bag.transform.childCount <= other.GetComponent<StackControl>().bag.transform.childCount)
            //    {
            //        StartCoroutine(PlayerFall());
            //    }
            //}
        }
    }

    private void AddStack(GameObject stack)
    {
        Vector3 stackPos = stack.transform.localPosition;
        int x = ((int)stackPos.x + 8) / 4;
        int z = ((int)stackPos.z + 16) / 4;
        properties.currentStackSpawner.GetComponent<StackSpawner>().hasStack[x][z] = false;

        stack.transform.SetParent(bag.transform);
        if (prevStack)
        {
            Vector3 newPos = prevStack.transform.localPosition;
            newPos.y += 0.5f;
            stack.transform.localPosition = newPos;
        }
        else
        {
            stack.transform.localPosition = bag.transform.localPosition;
        }
        stack.transform.localRotation = Quaternion.identity;
        stack.tag = StringCache.Untagged;
        prevStack = stack;
        properties.targetStacks.Remove(stack);
        properties.hasTarget = false;
    }

    public void RemoveStack()
    {
        int stackCnt= bag.transform.childCount;
        if (stackCnt == 0)
        {
            Debug.Log("Cannot remove stack");
            return;
        }

        GameObject topStack = bag.transform.GetChild(stackCnt-1).gameObject;
        if(stackCnt == 1)
        {
            prevStack = null;
        }
        if(stackCnt > 1)
        {
            prevStack = bag.transform.GetChild(stackCnt - 2).gameObject;
        }
        topStack.transform.SetParent(properties.currentStackSpawner.transform);
        topStack.transform.localPosition = properties.currentStackSpawner.GetComponent<StackSpawner>().RandomSpawnPoint();
        topStack.transform.localRotation = Quaternion.identity;
        topStack.tag = StringCache.Stack;
    }

    public void RemoveAllStack()
    {
        int stackCnt = bag.transform.childCount;
        if (stackCnt == 0) return;

        foreach(Transform child in bag.transform)
        {
            StackFall(child.gameObject);
        }
        prevStack = null;
    }

    public void StackFall(GameObject stack)
    {
        float x = Random.Range(-1, 1) * stackFallRadius;
        float z = Random.Range(-1, 1) * stackFallRadius;
        Vector3 fallPos = new Vector3(x, -0.06f, z) + new Vector3(transform.position.x, 0, transform.position.z);
        stack.transform.SetParent(properties.currentStackSpawner.transform);
        stack.transform.localPosition = fallPos;
        stack.transform.localRotation = Quaternion.identity;
        stack.tag = StringCache.Stack;
    }

    public IEnumerator PlayerFall()
    {
        RemoveAllStack();
        properties.characterState = CharacterState.Fall;
        Animator anim = properties.animator;
        bool isRunning = anim.GetBool(StringCache.IsRunning);
        if (anim.GetInteger(StringCache.Fall) != 0)
        {
            anim.SetInteger(StringCache.Fall, 0);
        }
        yield return new WaitForSeconds(2f);
        properties.characterState = CharacterState.ClaimStack;
        if (isRunning)
        {
            anim.SetInteger(StringCache.Fall, 1);
        }
        else
        {
            anim.SetInteger(StringCache.Fall, -1);
        }
    }
}