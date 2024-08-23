using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public float speed = 5.0f;

    public Vector2 offset = Vector2.zero;

    void FixedUpdate()
    {
        Vector2 position = Vector2.Lerp((Vector2)transform.position, (Vector2)target.position + offset, speed * Time.deltaTime);
        transform.position = new Vector3(position.x, position.y, -10);
    }
}