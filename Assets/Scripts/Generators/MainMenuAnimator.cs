using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuAnimation : MonoBehaviour
{
    public Image uiImage; // Assign the UI Image here
    public Color[] colors; // Array of colors for prefabs
    public GameObject[] prefabs; // Array of prefabs
    public float fadeDuration = 1f; // Duration of fade in/out
    public int prefabCount = 100; // Number of prefabs to instantiate
    public float repeatInterval = 10f; // Interval to repeat the animation

    private void Start()
    {
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        while (true) // Loop to repeat the animation
        {
            // Fade to black
            yield return FadeImage(1f, fadeDuration);

            // Remove all existing prefabs
            foreach (var obj in GameObject.FindGameObjectsWithTag("Shape"))
            {
                Destroy(obj);
            }

            int id = Random.Range(0, prefabs.Length);
            GameObject prefab = prefabs[id];
            for (int i = 0; i < prefabCount; i++)
            {
                GameObject instance = Instantiate(prefab, RandomPosition(), Quaternion.identity);
                instance.transform.localScale = RandomScale();
                SpriteRenderer renderer = instance.GetComponentInChildren<SpriteRenderer>();
                if (renderer != null)
                {
                    renderer.color = colors[id];
                    renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, Random.Range(0.2f, 1f));
                }
            }

            // Fade back to transparent
            yield return FadeImage(0f, fadeDuration);

            // Wait for the specified interval before repeating
            yield return new WaitForSeconds(repeatInterval);
        }
    }

    private IEnumerator FadeImage(float targetAlpha, float duration)
    {
        Color startColor = uiImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            uiImage.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        uiImage.color = endColor;
    }

    private Vector3 RandomPosition()
    {
        // Adjust the range as needed
        return new Vector3(Random.Range(-20f, 20f), Random.Range(-10f, 10f), 0f);
    }

    private Vector3 RandomScale()
    {
        float scale = Random.Range(0.5f, 4f);
        return new Vector3(scale, scale, 1f);
    }
}
