using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridge : MonoBehaviour
{
    public Transform bag;
    public Properties properties;
    public StackControl stackControl;

    private void Update()
    {
        Build();
    }
    public void Build()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag(StringCache.Step))
            {
                if(bag.childCount > 0)
                {
                    hit.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
                    if (!hit.transform.GetComponent<MeshRenderer>().enabled)
                    {
                        hit.transform.GetComponent<MeshRenderer>().enabled = true;
                    }

                    if (!hit.transform.GetComponent<MeshRenderer>().material.color.Equals(properties.color))
                    {
                        hit.transform.GetComponent<MeshRenderer>().material.color = properties.color;
                        stackControl.RemoveStack();
                    }
                }
                if(bag.childCount == 0)
                {
                    StartCoroutine(properties.findCurrentTargetStacks());
                    properties.hasTarget = false;
                    if (!properties.isBot)
                    {
                        if (!hit.transform.GetComponent<MeshRenderer>().material.color.Equals(properties.color))
                        {
                            hit.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
                            gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
