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

        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
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