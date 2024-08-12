using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] colorBlocks;
    public List<GameObject> spawnColorBlocks = new();
    public GameObject[] crystals;
    public GameObject[] clouds;
    private int[] crystalProbs = { 50, 59, 68, 77, 86, 93, 100 };
    private int _blocksCount = 15;
    private float _probability;
    private int _crystalProbability;
    private int spawnCount = 0;

    void Start()

    {
        SpawnBlocks();
        SpawnCrystals();
        SpawnClouds();
    }

    private void Update()
    {
        if ((transform.position - spawnColorBlocks[0].transform.position).magnitude > 7)
        {
            MoveBlocks();
        }
    }


    private void SpawnBlocks()
    {

        for (int j = 1; j < _blocksCount + 1; j++)
        {
            for (int i = 0; i < colorBlocks.Length; i++)
            {
                // float firstBlockPositionZ = spawnCount * _blocksCount * colorBlocks[0].transform.position.z;
                //Vector3 firstBlock = new Vector3(firstBlockPositionZ);
                Vector3 blockPosition = colorBlocks[i].transform.position;
                Vector3 newBlockPosition = blockPosition + 3 * j * Vector3.forward;
                GameObject newColorBlock = Instantiate(colorBlocks[i], newBlockPosition, Quaternion.identity);
                spawnColorBlocks.Add(newColorBlock);
            }
        }

        spawnCount += 10;
    }

    private void SpawnCrystals()
    {
        for (int j = 1; j < _blocksCount + 1; j++)
        {
            for (int i = 0; i < colorBlocks.Length; i++)
            {
                Vector3 blockPosition = colorBlocks[i].transform.position;
                Vector3 newBlockPosition = blockPosition + new Vector3(0, 1, 3 * j);

                if (Random.value > 0.7f)
                {
                    int randomIndex = GetRandomCrystalType();
                    Instantiate(crystals[randomIndex], newBlockPosition, Quaternion.identity);
                }
            }
        }
    }

    private void SpawnClouds()
    {
        for (int j = 1; j < _blocksCount + 1; j++)
        {
            for (int i = 0; i < clouds.Length; i++)
            {
                Vector3 newPosition = clouds[i].transform.position + j * 6 * Vector3.forward;
                Instantiate(clouds[i], newPosition, Quaternion.identity);
            }
        }
    }


    private void MoveBlocks()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject blockToMove = spawnColorBlocks[0];
            spawnColorBlocks.RemoveAt(0);
            blockToMove.transform.position = spawnColorBlocks[^4].transform.position + 3 * Vector3.forward;
            spawnColorBlocks.Add(blockToMove);
            SpawnNewCrystals(blockToMove.transform.position);
            SpawnNewClouds(blockToMove.transform.position.z);
        }


    }

    private void SpawnNewCrystals(Vector3 position)
    {
        if (Random.value > 0.7f)
        {
            int randomIndex = GetRandomCrystalType();
            Instantiate(crystals[randomIndex], position + Vector3.up, Quaternion.identity);
        }

    }

    private void SpawnNewClouds(float z)
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            Vector3 newPosition = clouds[i].transform.position + z * Vector3.forward;
            Instantiate(clouds[i], newPosition, Quaternion.identity);
        }
    }


    private int GetRandomCrystalType()
    {
        float probability = Random.Range(0, 100);

        for (int i = 0; i < crystalProbs.Length; i++)
        {
            if (crystalProbs[i] > probability)
            {
                _crystalProbability = i;
                break;
            }

        }

        return _crystalProbability;

    }

}
