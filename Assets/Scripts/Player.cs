using UnityEngine;

public class Player : MonoBehaviour
{
    private HealthManager health;
    private EnergyManager energy;
    private HeatManager heat;
    private TeamManager team;
    private VelocityManager velocity;
    private InventoryManager inventory;

    [Header("Attributes")]
    [SerializeField] private float speed = 6.0f;

    // Health bar variables
    [Header("Health Bar")]
    [SerializeField] private GameObject hpFill;
    [SerializeField] private GameObject hpFill2;
    private float dynamicHpFill = 1f;
    private float dynamicHpFill2 = 1f;
    private float staticHpFill2 = 1f;
    private float timerHp = 1f;
    private float tempDynamicHpFill = 1f;

    // Energy bar variables
    [Header("Energy Bar")]
    [SerializeField] private GameObject energyFill;
    private float dynamicEnergyFill = 1f;

    // Item and ability variables
    [Header("Items")]
    private int currentItem = 1;

    // Body part variables
    [Header("Body Parts")]
    [SerializeField] private Transform leftBodyPart;
    [SerializeField] private Transform rightBodyPart;
    private float bodyPartOffset = 0.1f;

    private Rigidbody2D rb;

    private TooltipManager tooltipManager;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        tooltipManager = FindObjectOfType<TooltipManager>();
        health = GetComponent<HealthManager>();
        energy = GetComponent<EnergyManager>();
        heat = GetComponent<HeatManager>();
        team = GetComponent<TeamManager>();
        velocity = GetComponent<VelocityManager>();
        inventory = GetComponent<InventoryManager>();
    }

    void FixedUpdate()
    {
        HandleInput();
        UpdateHealthBar();
        UpdateEnergyBar();
        UpdateVelocity();
        UpdateBodyParts();
    }

    private void UpdateHealthBar()
    {
        dynamicHpFill = Mathf.Lerp(dynamicHpFill, health.health / health.maxHealth, Time.fixedDeltaTime * 15);
        if (tempDynamicHpFill != dynamicHpFill) {
            timerHp = 0.4f;
            tempDynamicHpFill = dynamicHpFill;
        }
        if (timerHp <= 0 && dynamicHpFill != dynamicHpFill2) staticHpFill2 = dynamicHpFill;
        else timerHp -= Time.fixedDeltaTime;

        dynamicHpFill2 = Mathf.Lerp(dynamicHpFill2, staticHpFill2, Time.fixedDeltaTime * 15);
        hpFill.transform.localScale = new Vector3(dynamicHpFill, 1.0f, 1.0f);
        hpFill2.transform.localScale = new Vector3(dynamicHpFill2, 1.0f, 1.0f);
    }

    private void UpdateEnergyBar()
    {
        dynamicEnergyFill = Mathf.Lerp(dynamicEnergyFill, energy.energy / energy.maxEnergy, Time.fixedDeltaTime * 15);
        energyFill.transform.localScale = new Vector3(dynamicEnergyFill, 1.0f, 1.0f);
    }

    private void HandleInput()
    {
        for (int i = 0; i < inventory.slots-1; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) currentItem = i+1;
        }
        if (Input.GetMouseButton(0))
        {
            if (inventory.UseSlot(currentItem, GetMouseDirection())) OffsetBodyParts(); 
        }
        if (Input.GetMouseButton(1))
        {
            if (inventory.UseSlot(0, GetMouseDirection())) OffsetBodyParts();
        }
    }

    private Vector2 GetMouseDirection()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.position;
    }

    private void OffsetBodyParts()
    {
        bodyPartOffset = Mathf.Clamp(bodyPartOffset+0.15f, 0f, 4f);
        // bodyPartOffset += 0.15f;
        // if (bodyPartOffset > 0.4f) bodyPartOffset = 0.4f;
    }

    private void UpdateVelocity()
    {
        velocity.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }

    private void UpdateBodyParts()
    {
        bodyPartOffset = Mathf.Lerp(bodyPartOffset, 0.1f, Time.fixedDeltaTime * 3);

        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.position).normalized;

        leftBodyPart.position = rb.position + new Vector2(-direction.y, direction.x) * bodyPartOffset;
        rightBodyPart.position = rb.position + new Vector2(direction.y, -direction.x) * bodyPartOffset;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        leftBodyPart.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 135));
        rightBodyPart.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 45));
    }
}
