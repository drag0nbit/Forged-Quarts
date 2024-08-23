using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] prefabs;
    public float scaleMultiplier = 2.0f;
    public float placementThreshold = 0.5f;
    public float noiseScale = 0.1f;

    public int mapWidth = 10;
    public int mapHeight = 10;
    public int seed = 1;
    public Color objectColor = Color.white;

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; mapHeight > y; y++)
            {
                float perlinValue = Mathf.PerlinNoise(seed + x * noiseScale, seed + y * noiseScale);
                if (perlinValue > placementThreshold)
                {
                    float scale = Mathf.Lerp(0f, scaleMultiplier, perlinValue - 0.5f);
                    if (Random.value > 0.9f && scale > 0.7f)
                    {
                        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
                        GameObject obj = Instantiate(prefab, new Vector3(x, y, 0), prefab.transform.rotation);
                        obj.transform.localScale = new Vector3(scale, scale, 1f);
                        SpriteRenderer spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();
                        if (spriteRenderer != null) spriteRenderer.color = new Color(objectColor.r, objectColor.g, objectColor.b, Random.Range(0.5f, 1f));
                    }
                }
            }
        }
    }
}