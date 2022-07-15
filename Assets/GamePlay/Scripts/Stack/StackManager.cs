using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public static StackManager instance;

    public List<Color> colors;

    public List<GameObject>[] stacks;

    private void Awake()
    {
        instance = this;
        stacks = new List<GameObject>[4];
        for(int i = 0; i < 4; i++)
        {
            stacks[i] = new List<GameObject>();
        }
    }
}
