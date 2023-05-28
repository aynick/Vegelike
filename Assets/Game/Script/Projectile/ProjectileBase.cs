using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] public Vector2 dir;
    [SerializeField] public float lifeTime;
    protected float time;
    [SerializeField] public float moveSpeed;
    [SerializeField] public int horizontalFlip;
    [SerializeField] protected Rigidbody2D rigidbody2D;
}
