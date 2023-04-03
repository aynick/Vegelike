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

        private Animator _animator;
        
        public EnemyIdleState(IStateSwitcher switcher,EnemyPlayerDetect enemyPlayerDetect,Rigidbody2D rigidbody2D,
            EnemyStats enemyStats,Animator animator) : base(switcher)
        {
            _animator = animator;
            _enemyStats = enemyStats;
            _rigidbody2D = rigidbody2D;
            _playerDetect = enemyPlayerDetect;
        }

        public override void Enter()
        {
            var randomTime = Random.Range(minTime, maxTime);
            time = randomTime;
            _animator.SetBool("Idle",true);
        }

        public override void Exit()
        {
            _animator.SetBool("Idle",false);
        }

        public override void FixedUpdate()
        {
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            time -= Time.fixedDeltaTime;
            if (time <= 0 && _playerDetect.closePlayer == null)
            {
                _switcher.Switch<EnemyPatrolState>();
            }
            if (_playerDetect.closePlayer != null && !_enemyStats.isOnCliff && _enemyStats.isGround)
            { 
                _switcher.Switch<EnemyChaseState>();
            } 
            
        }

        public override void Update()
        {
            
        }
    }
}