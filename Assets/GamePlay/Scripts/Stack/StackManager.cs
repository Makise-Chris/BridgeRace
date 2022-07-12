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
        RefreshStacks();
    }

    public void RefreshStacks()
    {
        GameObject[] allStacks = GameObject.FindGameObjectsWithTag(StringCache.Stack);
        for (int i = 0; i < allStacks.Length; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if (allStacks[i].GetComponent<Renderer>().material.color.Equals(colors[j]))
                {
                    stacks[j].Add(allStacks[i]);
                }
            }
        }
    }
}
