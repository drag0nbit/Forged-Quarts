using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifetime = 5f;
    public bool active = true;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    public void Activate()
    {
        Destroy(gameObject, lifetime);
    }
}