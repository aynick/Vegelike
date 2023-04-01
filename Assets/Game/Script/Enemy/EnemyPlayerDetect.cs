using System;
using UnityEngine;

namespace Game.Script
{
    public class EnemyPlayerDetect : MonoBehaviour
    {
        [SerializeField] private Transform _playerDetect;
        [SerializeField] private float _playerDetectRadius;
        [SerializeField] private LayerMask playerLayer;
        private float _minDistance = Mathf.Infinity;

        public Vector2 closePlayerPos { private set; get; }
        
        private void Start()
        {
            closePlayerPos = Vector2.zero;
        }

        private void FixedUpdate()
        {
            _minDistance = Mathf.Infinity;
            closePlayerPos = Vector2.zero;
            var players = Physics2D.OverlapCircleAll(_playerDetect.position, _playerDetectRadius,playerLayer);
            foreach (var player in players) 
            { 

                Vector2 targetPlayerPos = player.transform.position;
                if (targetPlayerPos.y < transform.position.y - 1 || targetPlayerPos.y > transform.position.y + 1)
                {
                    return;
                }
                float distance = Vector2.Distance(closePlayerPos, targetPlayerPos); 
                if (Mathf.Round(distance) < Mathf.Round(_minDistance)) 
                { 
                    _minDistance = distance; 
                    closePlayerPos = targetPlayerPos;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_playerDetect.position,_playerDetectRadius);
        }
    }
}