using System;
using UnityEngine;

namespace Game.Script
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private Transform checkGround;
        [SerializeField] private LayerMask groundMask;
        private Rigidbody2D rigidbody2D;

        public bool canMove;
        public bool isGround { private set; get; }

        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            isGround = Physics2D.OverlapCircle(checkGround.position, 0.1f, groundMask);
            if (rigidbody2D.velocity.normalized == Vector2.up) isGround = false;
        }
    }
}