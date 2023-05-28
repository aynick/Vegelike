using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : ProjectileBase
{
    [SerializeField] public int damage;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask attackLayer;
    private void FixedUpdate()
    {
        time -= Time.fixedDeltaTime;
        if (time<=0)
        {
            Destroy(gameObject);
        }
        rigidbody2D.velocity = dir*moveSpeed;
    }

    private void Start()
    {
        time = lifeTime;
        transform.localScale = new Vector3(horizontalFlip, transform.localScale.y);
    }

    public void Attack()
    {
        var colliders = Physics2D.OverlapCircleAll(attackPoint.position, radius,attackLayer);
        foreach (var collider in colliders)
        {
            collider.TryGetComponent(out EnemyDamagable enemyDamagable);
            enemyDamagable.ApplyDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position,radius);
    }
}
