using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSpawner : MonoBehaviour
{
    public int stackPerCharacter;
    public int CharacterCnt;
    public bool isStartStage;
    public List<string> stackTags;
    public bool[][] hasStack;
    public bool[] isSpawned;

    private void Start()
    {
        isSpawned = new bool[CharacterCnt];
        hasStack = new bool[5][];
        for(int i = 0; i < 5; i++)
        {
            hasStack[i] = new bool[9];
        }

        if (isStartStage)
        {
            StartCoroutine(SpawnStacks());
        }
    }

    public IEnumerator SpawnStacks()
    {
        yield return null; 
        for(int i = 0; i < stackPerCharacter; i++)
        {
            for(int j = 0; j < CharacterCnt; j++)
            {
                Vector3 spawnPos = RandomSpawnPoint();
                if (!spawnPos.Equals(Vector3.zero))
                {
                    GameObject newStack = ObjectPooler.instance.SpawnFromPool(stackTags[j]);
                    newStack.transform.SetParent(transform);
                    newStack.transform.localPosition = spawnPos;
                }
            }
        }
    }

    public IEnumerator SpawnCharacterStack(int index)
    {
        yield return null;
        if (!isSpawned[index])
        {
            for (int i = 0; i < stackPerCharacter; i++)
            {
                Vector3 spawnPos = RandomSpawnPoint();
                if (!spawnPos.Equals(Vector3.zero))
                {
                    GameObject newStack = ObjectPooler.instance.SpawnFromPool(stackTags[index]);
                    newStack.transform.SetParent(transform);
                    newStack.transform.localPosition = spawnPos;
                }
            }
            isSpawned[index] = true;
        }
    }

    public Vector3 RandomSpawnPoint()
    {
        int xPos = Random.Range(-2, 3) * 4;
        int zPos = Random.Range(-4, 5) * 4;
        Vector3 spawnPos = new Vector3(xPos, -0.06f, zPos);
        int x = ((int)spawnPos.x + 8) / 4;
        int z = ((int)spawnPos.z + 16) / 4;
        if (hasStack[x][z])
        {
            spawnPos = findSpawnPoint();
        }
        else
        {
            hasStack[x][z] = true;
        }
        return spawnPos;
    }

    public Vector3 findSpawnPoint()
    {
        for(int i=0; i<5; i++)
        {
            for(int j=0;j<9; j++)
            {
                if (!hasStack[i][j])
                {
                    hasStack[i][j] = true;
                    return new Vector3(4 * i - 8,-0.06f , 4 * j - 16);
                }
            }
        }
        return Vector3.zero;
    }
}
