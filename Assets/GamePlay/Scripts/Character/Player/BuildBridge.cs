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
            if (hit.transform.CompareTag("Step"))
            {
                if(bag.childCount > 0)
                {
                    if (!hit.transform.GetComponent<MeshRenderer>().enabled)
                    {
                        if (hit.transform.GetChild(1).GetComponent<BoxCollider>().enabled)
                        {
                            hit.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                        }
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
                }
            }
        }
    }
}
