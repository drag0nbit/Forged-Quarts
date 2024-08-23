using UnityEngine;

public class MineralGenerator : MonoBehaviour
{
    public GameObject[] prefabs; // List of prefabs to place
    public float placementThreshold = 0.5f; // Threshold for placing objects
    public float noiseScale = 0.1f; // Scale of the Perlin noise

    public int mapWidth = 10; // Width of the map
    public int mapHeight = 10; // Height of the map

    public int seed = 1;

    void Start()
    {
        GenerateLevel();
    }
    void GenerateLevel()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float perlinValue = Mathf.PerlinNoise(seed + x * noiseScale, seed + y * noiseScale);

                if (perlinValue < placementThreshold)
                {
                    if (Random.value > 0.9f)
                    {
                        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
                        Vector3 position = new Vector3(x, y, 0);
                        GameObject obj = Instantiate(prefab, position, prefab.transform.rotation);
                    }
                }
            }
        }
    }
}