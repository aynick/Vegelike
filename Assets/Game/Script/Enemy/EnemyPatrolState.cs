using UnityEngine;

namespace Game.Script
{
    public class EnemyPatrolState : StateBase
    {
        
        private Rigidbody2D _rigidbody2D;
        private float _walkSpeed;
        private EnemyInfo enemyInfo;
        private Transform _enemyTransform;
        private EnemyPlayerDetect _playerDetect;
        private EnemyEventHandler _enemyEventHandler;
        
        private float time;
        private float maxTime = 4;
        private float minTime = 1;
        private Animator _animator;
        private float dir = 1;
        public EnemyPatrolState(IStateSwitcher switcher,Rigidbody2D rigidbody2D,float walkSpeed,EnemyInfo enemyInfo,
            Transform enemyTransform,EnemyPlayerDetect playerDetect,Animator animator,EnemyEventHandler enemyEventHandler) : base(switcher)
        {
            _enemyEventHandler = enemyEventHandler;
            _animator = animator;
            _playerDetect = playerDetect;
            _enemyTransform = enemyTransform;
            this.enemyInfo = enemyInfo;
            _rigidbody2D = rigidbody2D;
            _walkSpeed = walkSpeed;
        }

        public override void Enter()
        {
            _enemyEventHandler.OnAppliedDamage += OnDamaged;
            _enemyEventHandler.OnDestroyed += OnDestroy;
            var randomTime = Random.Range(minTime, maxTime);
            time = randomTime;
            _animator.SetBool("Move",true);
            var randomChanceToFlip = Random.Range(0, 1);
            if (randomChanceToFlip == 0)
            {
                Rotate();
            }
        }

        public override void Exit()
        {
            _enemyEventHandler.OnDestroyed -= OnDestroy;
            _enemyEventHandler.OnAppliedDamage -= OnDamaged;
            _animator.SetBool("Move",false);
        }

        public override void FixedUpdate()
        {
            if (_playerDetect.closePlayer != null)
            {
                _switcher.Switch<EnemyChaseState>();
            }
            time -= Time.fixedDeltaTime;
            if (time <= 0) _switcher.Switch<EnemyIdleState>();
            if (enemyInfo.isOnCliff) Rotate();
            _rigidbody2D.velocity = new Vector2(_walkSpeed * dir,_rigidbody2D.velocity.y);
        }

        private void Rotate()
        {
            dir *= -1;
            _enemyTransform.localScale = new Vector3(_enemyTransform.localScale.x *-1,
                        _enemyTransform.localScale.y, _enemyTransform.localScale.z);
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