using System;
using UnityEngine;

namespace Game.Script
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private Transform checkGround;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private PlayerEventHandler playerEventHandler;
        private Rigidbody2D rigidbody2D;
        private int healthPoint;

        public bool canMove;
        public bool isInvulnerable;
        public bool isGround { private set; get; }

        public void ExternalInit(int hp)
        {
            healthPoint = hp;
        }

        private void ApplyDamage(int damage)
        {
            if (healthPoint - damage <= 0)
            {
                healthPoint = 0;
                Die();
                playerEventHandler.OnHealthPointChange(healthPoint);
                return;
            }
            healthPoint -= damage;
            playerEventHandler.OnHealthPointChange(healthPoint);
        }

        private void Die()
        {
            Debug.Log("Player is dead");
        }

        private void Start()
        {
            playerEventHandler.OnAppliedDamage += ApplyDamage;
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            isGround = Physics2D.OverlapCircle(checkGround.position, 0.1f, groundMask);
            if (rigidbody2D.velocity.normalized == Vector2.up) isGround = false;
        }
    }
}