using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityManager : MonoBehaviour
{
    private Vector2 velocity = Vector2.zero;
    private Vector2 knockback = Vector2.zero;
    private bool forceAdded = false;
    public float speed;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        UpdateKnockback();
        if (!forceAdded) UpdateVelocity();
        Move();
        forceAdded = false;
    }

    public void AddForce(Vector2 direction)
    {
        direction = direction.normalized;
        velocity = Vector2.Lerp(velocity, direction * speed, Time.fixedDeltaTime * 7);
        forceAdded = true;
    }

    public void AddKnockback(Vector2 direction, float amp)
    {
        direction = direction.normalized;
        knockback += direction * amp;
    }

    private void UpdateKnockback()
    {
        knockback = Vector2.Lerp(knockback, Vector2.zero, Time.fixedDeltaTime * 7);
    }

    private void UpdateVelocity()
    {
        velocity = Vector2.Lerp(velocity, Vector2.zero, Time.fixedDeltaTime * 7);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + (velocity + knockback) * Time.fixedDeltaTime);
    }
}
