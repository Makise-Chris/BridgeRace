using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    public int index;
    public List<GameObject> targetStacks;
    public Color color;
    public Animator animator;
    public bool hasTarget;

    private void Start()
    {
        targetStacks = StackManager.instance.stacks[index];
        hasTarget = false;
    }
}
