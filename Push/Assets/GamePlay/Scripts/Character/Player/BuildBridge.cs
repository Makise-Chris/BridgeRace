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
                MeshRenderer hitMR = hit.transform.GetComponent<MeshRenderer>();
                BoxCollider hitBox = hit.transform.GetChild(0).GetComponent<BoxCollider>();
                if (hitMR != null)
                {
                    if (bag.childCount > 0)
                    {
                        hitBox.enabled = false;

                        if (!hitMR.enabled)
                        {
                            hitMR.enabled = true;
                        }

                        if (!hitMR.material.color.Equals(properties.color))
                        {
                            hitMR.material.color = properties.color;
                            stackControl.RemoveStack();
                        }
                    }
                    if (bag.childCount == 0)
                    {
                        StartCoroutine(properties.findCurrentTargetStacks());
                        properties.hasTarget = false;
                        if (!properties.isBot)
                        {
                            if (!hitMR.material.color.Equals(properties.color))
                            {
                                hitBox.enabled = true;
                                gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }
}
