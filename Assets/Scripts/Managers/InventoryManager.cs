using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private HealthManager health;
    private EnergyManager energy;
    private HeatManager heat;
    private TeamManager team;
    private VelocityManager velocity;
    private Rigidbody2D rb;
    
    public int slots = 1;
    [SerializeField] private Item[] inventory;
    private float[] cooldowns;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthManager>();
        energy = GetComponent<EnergyManager>();
        heat = GetComponent<HeatManager>();
        team = GetComponent<TeamManager>();
        velocity = GetComponent<VelocityManager>();
        Resize(slots);
    }

    void Update()
    {
        UpdateCooldown();
    }

    private void Resize(int s)
    {
        System.Array.Resize(ref inventory, slots);
        System.Array.Resize(ref cooldowns, slots);
    }

    public bool UseSlot(int slot, Vector2 direction)
    {
        direction = direction.normalized;
        Item item = GetItemInSlot(slot);
        if (item == null || cooldowns[slot] > 0 || !EnoughCost(item.cost, item.costHealth)) return false;
        if (item.spawn != null) Spawn(item.spawn, direction, item.radius);
        velocity.AddKnockback(-direction, item.knockback);
        ApplyCost(item.cost, item.costHealth);
        cooldowns[slot] = item.cooldown;
        return true;
    }

    public Item GetItemInSlot(int slot)
    {
        if (slot < 0 || slot >= slots) return null;
        return inventory[slot];
    }

    void Spawn(GameObject spawnObject, Vector2 direction, float radius)
    {
        Vector2 spawnPosition = rb.position;
        if (radius != 0) spawnPosition += direction * radius;
        GameObject bullet = Instantiate(spawnObject, spawnPosition, Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)));
        bullet.GetComponent<TeamManager>()?.Set(team.team);
    }

    public bool EnoughCost(float cost, bool blood)
    {
        if (blood) return health.health > cost;
        else return energy.energy >= cost;
    }

    private void ApplyCost(float cost, bool blood)
    {
        if (health != null && blood) health.Damage(cost);
        else if (energy != null) energy.Charge(-cost);
    }

    private void UpdateCooldown()
    {
        for (int i = 0; i < cooldowns.Length; i++)
        {
            if (cooldowns[i] > 0) cooldowns[i] -= Time.deltaTime;
        }
    }

    public float GetCooldownInSlot(int slot)
    {
        Item item = GetItemInSlot(slot);
        if (item == null) return 0.00001f;
        return cooldowns[slot];
    }
}
