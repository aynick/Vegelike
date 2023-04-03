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

        public PlayerBehavior closePlayer { private set; get; }
        private Vector2 closePlayerPos = Vector2.zero;

        private void Start()
        {
            closePlayer = null;
        }

        private void FixedUpdate()
        {
            _minDistance = Mathf.Infinity;
            closePlayer = null;
            closePlayerPos = Vector2.zero;
            var players = Physics2D.OverlapCircleAll(_playerDetect.position, _playerDetectRadius,playerLayer);
            foreach (var player in players)
            {
                player.TryGetComponent(out PlayerBehavior playerBehavior);
                if (playerBehavior == null) return;
                var targetPlayerPos = playerBehavior;
                if (targetPlayerPos.transform.position.y < transform.position.y - 1 || targetPlayerPos.transform.position.y > transform.position.y + 1)
                {
                    return;
                }
                float distance = Vector2.Distance(closePlayerPos, targetPlayerPos.transform.position); 
                if (Mathf.Round(distance) < Mathf.Round(_minDistance)) 
                { 
                    _minDistance = distance; 
                    closePlayer = targetPlayerPos;
                    closePlayerPos = targetPlayerPos.transform.position;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_playerDetect.position,_playerDetectRadius);
        }
    }
}