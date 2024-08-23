using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
