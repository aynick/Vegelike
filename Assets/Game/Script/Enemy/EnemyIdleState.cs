using UnityEngine;
namespace Game.Script
{
    public class EnemyIdleState : StateBase
    {
        private EnemyPlayerDetect _playerDetect;
        private Rigidbody2D _rigidbody2D;
        private EnemyStats _enemyStats;
        
        private float maxTime = 4;
        private float minTime = 1;
        private float time;
        public EnemyIdleState(IStateSwitcher switcher,EnemyPlayerDetect enemyPlayerDetect,Rigidbody2D rigidbody2D,EnemyStats enemyStats) : base(switcher)
        {
            _enemyStats = enemyStats;
            _rigidbody2D = rigidbody2D;
            _playerDetect = enemyPlayerDetect;
        }

        public override void Enter()
        {
            var randomTime = Random.Range(minTime, maxTime);
            time = randomTime;
        }

        public override void Exit()
        {
        }

        public override void FixedUpdate()
        {
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            time -= Time.fixedDeltaTime;
            if (time <= 0 && _playerDetect.closePlayerPos == Vector2.zero)
            {
                _switcher.Switch<EnemyPatrolState>();
            }
            if (_playerDetect.closePlayerPos != Vector2.zero && !_enemyStats.isOnCliff && _enemyStats.isGround)
            { 
                _switcher.Switch<EnemyChaseState>();
            } 
            
        }

        public override void Update()
        {
            
        }
    }
}