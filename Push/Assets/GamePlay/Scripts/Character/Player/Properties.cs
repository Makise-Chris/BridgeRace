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
    public bool isBot;

    public GameObject currentStage;
    public GameObject currentStackSpawner;
    public GameObject currentTargetStacks;
    public GameObject currentBridges;
    public GameObject targetBridge;

    public CharacterState characterState;

    public virtual void Start()
    {
        characterState = CharacterState.ClaimStack;
        targetStacks = StackManager.instance.stacks[index];
        findCurrentStage();
        StartCoroutine(findCurrentTargetStacks());
        hasTarget = false;
    }

    public void findCurrentStage()
    {
        currentStackSpawner = currentStage.transform.GetChild(1).gameObject;
        currentBridges = currentStage.transform.GetChild(0).gameObject;

        int target = Random.Range(0, currentBridges.transform.childCount);
        targetBridge = currentBridges.transform.GetChild(target).GetChild(0).gameObject;
        currentTargetStacks = currentStage.transform.GetChild(1).gameObject;
    }

    public IEnumerator findCurrentTargetStacks()
    {
        yield return new WaitForSeconds(0.3f);
        targetStacks.Clear();
        int length = currentTargetStacks.transform.childCount;
        for(int i=0; i < length; i++)
        {
            if (currentTargetStacks.transform.GetChild(i).GetComponent<Renderer>().material.color.Equals(color))
            {
                targetStacks.Add(currentTargetStacks.transform.GetChild(i).gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringCache.NextStage))
        {
            currentStage = other.GetComponent<ChangeState>().state;
            findCurrentStage();

            StartCoroutine(currentStackSpawner.GetComponent<StackSpawner>().SpawnCharacterStack(index));
            StartCoroutine(findCurrentTargetStacks());
            hasTarget = false;
        }
        if (other.CompareTag(StringCache.WinPos))
        {
            if (isBot)
            {
                UIManager.instance.LosePanel();
            }
            else
            {
                UIManager.instance.WinPanel();
            }
        }
    }
}

[System.Serializable]
public enum CharacterState
{
    ClaimStack,
    BuildBridge,
    Fall
}