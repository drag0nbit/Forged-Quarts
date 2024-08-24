using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private VelocityManager velocity;
    private InventoryManager inventory;
    private TeamManager team;
    private Rigidbody2D rb;

    private HashSet<GameObject> objectsInTrigger = new HashSet<GameObject>();

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = GetComponent<VelocityManager>();
        inventory = GetComponent<InventoryManager>();
        team = GetComponent<TeamManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TeamManager t = other.gameObject.GetComponent<TeamManager>();
        if (t != null && !t.Compare(team.team) && !other.CompareTag("Projectile")) objectsInTrigger.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        objectsInTrigger.Remove(other.gameObject);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (objectsInTrigger.Count == 0) return;

        GameObject nearestObject = null;
        float shortestDistance = float.MaxValue;

        foreach (GameObject obj in objectsInTrigger)
        {
            float distance = Vector2.Distance(transform.position, obj.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestObject = obj;
            }
        }

        if (nearestObject != null)
        {
            Vector2 direction = nearestObject.transform.position - transform.position;
            velocity.AddForce(direction * 10f);
            inventory.UseSlot(0, direction);
        }
    }

}
