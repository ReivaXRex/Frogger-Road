using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int minDistanceFromPlayer;
    [SerializeField] private int maxTerrainSize;
    [SerializeField] private List<TerrainData> terrainData = new List<TerrainData>();
    [SerializeField] private Transform terrainHolder;

    private List<GameObject> currentTerrain = new List<GameObject>();
    [HideInInspector] public Vector3 currentPos = new Vector3(0, 0, 0);

    private void Start()
    {
        // Spawn a block off terrain until the maxTerrainSize is hit.
        for (int i = 0; i < maxTerrainSize; i++)
        {
            SpawnTerrain(true, new Vector3(0, 0, 0));
        }

        maxTerrainSize = currentTerrain.Count;
    }

    /// <summary>
    /// Spawn a block of Terrain.
    /// </summary>
    public void SpawnTerrain(bool isStart, Vector3 playerPos)
    {
        if ((currentPos.x - playerPos.x < minDistanceFromPlayer) || isStart)
        {
            // Variable to hold which terrain to spawn.
            int whichTerrain = Random.Range(0, terrainData.Count);
            // How many of that terrain are going to spawn in a row.
            int terrainInSuccession = Random.Range(1, terrainData[whichTerrain].maxInSuccession);

            for (int i = 0; i < terrainInSuccession; i++)
            {
                int randomTerrain = Random.Range(0, terrainData[whichTerrain].possibleTerrain.Count);
                GameObject terrainToSpawn = terrainData[whichTerrain].possibleTerrain[randomTerrain];

                // Spawn a random block of terrain, store a reference to the spawned block.
                GameObject terrain = Instantiate(terrainToSpawn, currentPos, Quaternion.identity);
                terrain.transform.SetParent(terrainHolder);

                // Add the spawned terrain to the currentTerrain List.
                currentTerrain.Add(terrain);

                if (!isStart)
                {
                    // If the current number of terrain blocks exceeds the limit..
                    if (currentTerrain.Count > maxTerrainSize)
                    {
                        // Delete the block and remove it from the List.
                        Destroy(currentTerrain[0]);
                        currentTerrain.RemoveAt(0);
                    }
                }

                // Add an offset to the x axis so the blocks spawn 2 units apart (next to each other).
                currentPos.x += 2;
            }
        }
    }
}