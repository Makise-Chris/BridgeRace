using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackControl : MonoBehaviour
{
    public Properties properties;
    public GameObject bag;
    public GameObject prevStack;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(StringCache.Stack) && other.GetComponent<Renderer>().material.color.Equals(properties.color))
        {
            AddStack(other.gameObject);
        }
    }

    private void AddStack(GameObject stack)
    {
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
}
