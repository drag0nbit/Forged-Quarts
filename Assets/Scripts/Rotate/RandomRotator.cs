using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public float minRotation = 0f;
    public float maxRotation = 360f;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(minRotation, maxRotation));
    }
}
