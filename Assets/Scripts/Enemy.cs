using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private TeamManager team;
    public Item weapon;
    public float speed;
    private float cooldown;
    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private Vector2 knockback = Vector2.zero;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        team = GetComponent<TeamManager>();
    }

    void FixedUpdate()
    {
        Move();
        if (SeesPlayer())
        {
            if (cooldown <= 0 && SeesPlayer() && weapon != null) UseItem(weapon);
            else cooldown -= Time.deltaTime;
        }
    }

    private bool SeesPlayer()
    {
        RaycastHit2D hit;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        hit = Physics2D.Raycast(transform.position+direction, player.transform.position - transform.position);
        Debug.DrawRay(transform.position+direction, player.transform.position - transform.position, Color.green);
        return hit.collider != null && hit.transform.gameObject == player && hit.distance < 7;
    }

    void Move()
    {
        Vector3 direction = Vector3.zero;
        if (SeesPlayer()) direction = (player.transform.position - transform.position).normalized;
        velocity = Vector2.Lerp(velocity, direction * speed * Time.deltaTime, Time.fixedDeltaTime * 7);
        knockback = Vector2.Lerp(knockback, Vector2.zero, Time.fixedDeltaTime * 7);
        rb.MovePosition(rb.position + (velocity + knockback) * Time.fixedDeltaTime);
    }

    void UseItem(Item item)
    {
        if (item.spawn != null) Spawn(item.spawn, item.radius);
        cooldown = weapon.cooldown;
    }

    void Spawn(GameObject spawnObject, float radius)
    {
        Vector2 spawnPosition = transform.position;
        if (radius != 0) spawnPosition += ((Vector2)player.transform.position - rb.position).normalized * radius;
        knockback -= weapon.knockback*((Vector2)player.transform.position - rb.position).normalized;
        GameObject bullet = Instantiate(spawnObject, spawnPosition, Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2((player.transform.position - transform.position).y, (player.transform.position - transform.position).x) * Mathf.Rad2Deg)));
        bullet.GetComponent<TeamManager>()?.Set(team.team);
    }
}
