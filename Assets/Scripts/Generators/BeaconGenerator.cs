using UnityEngine;
using System.Collections.Generic;

public class BeaconGenerator : MonoBehaviour
{
    public GameObject prefab; // Beacon prefab to place
    public float placementThreshold = 0.5f; // Threshold for placing objects
    public float noiseScale = 0.1f; // Scale of the Perlin noise
    public int mapWidth = 10; // Width of the map
    public int mapHeight = 10; // Height of the map
    public int beaconCount = 10; // Number of beacons to place
    public float minDistanceBetweenBeacons = 2f; // Minimum distance between beacons

    public int seed = 1;

    private List<Vector3> beaconPositions = new List<Vector3>();

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        List<Vector3> potentialPositions = new List<Vector3>();

        // Generate a list of potential beacon positions
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                potentialPositions.Add(new Vector3(x, y, 0));
            }
        }

        // Shuffle the list to randomize the positions
        Shuffle(potentialPositions);

        int placedBeacons = 0;

        // Try to place beacons at random positions
        foreach (Vector3 position in potentialPositions)
        {
            if (placedBeacons >= beaconCount) return;

            float perlinValue = Mathf.PerlinNoise(seed + position.x * noiseScale, seed + position.y * noiseScale);

            if (perlinValue < placementThreshold && IsValidPosition(position))
            {
                Instantiate(prefab, position, prefab.transform.rotation);
                beaconPositions.Add(position);
                placedBeacons++;
            }
        }
    }

    bool IsValidPosition(Vector3 position)
    {
        foreach (Vector3 beaconPos in beaconPositions)
        {
            if (Vector3.Distance(position, beaconPos) < minDistanceBetweenBeacons)
            {
                return false;
            }
        }
        return true;
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random(seed);
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
