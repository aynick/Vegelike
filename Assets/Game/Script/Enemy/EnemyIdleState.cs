using UnityEngine;
namespace Game.Script
{
    public class EnemyIdleState : StateBase
    {
        private EnemyPlayerDetect _playerDetect;
        private Rigidbody2D _rigidbody2D;
        private EnemyInfo enemyInfo;
        
        private float maxTime = 4;
        private float minTime = 1;
        private float time;

        private Animator _animator;

        private EnemyEventHandler _enemyEventHandler;
        
        public EnemyIdleState(IStateSwitcher switcher,EnemyPlayerDetect enemyPlayerDetect,Rigidbody2D rigidbody2D,
            EnemyInfo enemyInfo,Animator animator,EnemyEventHandler enemyEventHandler) : base(switcher)
        {
            _enemyEventHandler = enemyEventHandler;
            _animator = animator;
            this.enemyInfo = enemyInfo;
            _rigidbody2D = rigidbody2D;
            _playerDetect = enemyPlayerDetect;
        }

        public override void Enter()
        {
            _enemyEventHandler.OnDestroyed += OnDestroy;
            _enemyEventHandler.OnAppliedDamage += OnDamaged;
            var randomTime = Random.Range(minTime, maxTime);
            time = randomTime;
            _animator.SetBool("Idle",true);
        }

        public override void Exit()
        {
            _enemyEventHandler.OnDestroyed -= OnDestroy;
            _enemyEventHandler.OnAppliedDamage -= OnDamaged;
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
            if (_playerDetect.closePlayer != null && !enemyInfo.isOnCliff && enemyInfo.isGround)
            { 
                _switcher.Switch<EnemyChaseState>();
            } 
            
        }

        public override void Update()
        {
            
        }
        private void OnDamaged(int dmg)
        {
            _switcher.Switch<EnemyDamagedState>();
        }

        private void OnDestroy()
        {
            _enemyEventHandler.OnDestroyed -= OnDestroy;
            _enemyEventHandler.OnAppliedDamage -= OnDamaged;
        }
    }
}