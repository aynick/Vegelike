using System;
using UnityEngine;

namespace Game.Script
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private Transform checkGround;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float groundCheckRadius;
        private Rigidbody2D rigidbody2D;

        public bool canMove;
        public bool isInvulnerable;
        public bool isGround;
        [SerializeField] public int maxHealthPoint;
        public int healthPoint;
        [SerializeField]private PlayerEventHandler playerEventHandler;
        
        private void ApplyDamage(int damage)
        {
            if (healthPoint - damage <= 0)
            {
                healthPoint = 0;
                Die();
                playerEventHandler.OnHealthPointChange(healthPoint,maxHealthPoint);
                return;
            }
            healthPoint -= damage;
            playerEventHandler.OnHealthPointChange(healthPoint,maxHealthPoint);
        }

        private void Die()
        {
            
        }

        public void Heal(int hp)
        {
            if (healthPoint + hp >= maxHealthPoint)
            {
                healthPoint = maxHealthPoint;
                playerEventHandler.OnHealthPointChange(healthPoint,maxHealthPoint);
                return;
            }
            healthPoint += hp;
            playerEventHandler.OnHealthPointChange(healthPoint,maxHealthPoint);
        }

        private void Start()
        {
            healthPoint = maxHealthPoint;
            playerEventHandler.OnHealthPointChange(healthPoint,maxHealthPoint);
            playerEventHandler.OnAppliedDamage += ApplyDamage;
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Heal(20);
            }
        }

        private void FixedUpdate()
        {
            isGround = Physics2D.OverlapCircle(checkGround.position, groundCheckRadius, groundMask);
            if (rigidbody2D.velocity.y > 2) isGround = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(checkGround.position,groundCheckRadius);
        }
    }
}