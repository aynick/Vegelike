using UnityEngine;

namespace Game.Script
{
    public class EnemyChaseState : StateBase
    {
        private EnemyPlayerDetect _playerDetect;
        private Rigidbody2D _rigidbody2D;
        private float _runSpeed;
        private Transform _enemyTransform;
        private EnemyStats _enemyStats;
        
        public EnemyChaseState(IStateSwitcher switcher,EnemyPlayerDetect playerDetect,Rigidbody2D rigidbody2D,float runSpeed,
            Transform enemyTransform,EnemyStats enemyStats) : base(switcher)
        {
            _enemyStats = enemyStats;
            _enemyTransform = enemyTransform;
            _runSpeed = runSpeed;
            _playerDetect = playerDetect;
            _rigidbody2D = rigidbody2D;
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void FixedUpdate()
        {
            if (_playerDetect.closePlayerPos == Vector2.zero)
            {
                _switcher.Switch<EnemyIdleState>();
            }
            if (_enemyStats.isOnCliff)
            {
                _switcher.Switch<EnemyIdleState>();
            }
            
            var dir = _playerDetect.closePlayerPos.x - _enemyTransform.position.x;
            dir = Mathf.Clamp(dir, -1, 1);
            if (dir < 0)
            {
                _enemyTransform.localScale = new Vector3(-1,
                    _enemyTransform.localScale.y, _enemyTransform.localScale.z);
            }
            if (dir > 0)
            {
                _enemyTransform.localScale = new Vector3(1,
                    _enemyTransform.localScale.y, _enemyTransform.localScale.z);
            }

            if (Vector2.Distance(_playerDetect.closePlayerPos, _enemyTransform.position) < 2)
            {
                return;
            }
            _rigidbody2D.velocity = new Vector2(dir * _runSpeed,_rigidbody2D.velocity.y);
        }
        

        public override void Update()
        {
        }
    }
}