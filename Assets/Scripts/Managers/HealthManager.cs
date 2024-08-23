using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float health = 10f;
    public float maxHealth = 10f;
    public bool destroyOnDeath = true;

    void Update()
    {
        if (health == 0 && destroyOnDeath) Destroy(gameObject);
    }

    public void Damage(float h)
    {
        health -= h;
        Clamp();
    }

    public void Set(float h)
    {
        health = h;
        Clamp();
    }

    private void Clamp()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
    }
}