using System;
using UnityEngine;

namespace Game.Script
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] private Transform _checkCliff;
        [SerializeField] private float _checkCliffDistance;
        [SerializeField] private Transform _checkGround;
        [SerializeField] private float _checkGroundRadius;
        [SerializeField] private LayerMask groundMask;

        public bool isGround { private set; get; }
        public bool isOnCliff { private set; get; }

        private void FixedUpdate()
        {
            isOnCliff = !Physics2D.Raycast(_checkCliff.position, Vector2.down,_checkCliffDistance,groundMask);
            isGround = Physics2D.OverlapCircle(_checkGround.position, _checkGroundRadius, groundMask);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_checkGround.position,_checkGroundRadius);
            Gizmos.DrawLine(_checkCliff.position,_checkCliff.position + Vector3.down * _checkCliffDistance);
        }
    }
}