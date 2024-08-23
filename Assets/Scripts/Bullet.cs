using UnityEngine;

public class Bullet : MonoBehaviour
{
    private TeamManager team; // 1 for player, -1 for enemy
    public float damage = 1f;

    void Awake()
    {
        team = GetComponent<TeamManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shape")) Destroy(gameObject);
        else
        {
            TeamManager t = collision.gameObject.GetComponent<TeamManager>();
            HealthManager h = collision.gameObject.GetComponent<HealthManager>();
            if (t != null && h != null && team != null && !team.Compare(t.team))
            {
                h.Damage(damage);
                Destroy(gameObject);
            }
        }
    }

    public void SetDamage(int t)
    {
        damage = t;
    }
}
