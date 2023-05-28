using UnityEngine;

namespace Game.Script
{
    public class EnemyChaseState : StateBase
    {
        private EnemyPlayerDetect _playerDetect;
        private Rigidbody2D _rigidbody2D;
        private float _runSpeed;
        private Transform _enemyTransform;
        private EnemyInfo enemyInfo;
        private Animator _animator;
        private float _attackDistance;
        private EnemyEventHandler _enemyEventHandler;
        
        public EnemyChaseState(IStateSwitcher switcher,EnemyPlayerDetect playerDetect,Rigidbody2D rigidbody2D,float runSpeed,
            Transform enemyTransform,EnemyInfo enemyInfo,Animator animator,EnemyEventHandler enemyEventHandler,
            float attackDistance) : base(switcher)
        {
            _attackDistance = attackDistance;
            _enemyEventHandler = enemyEventHandler;
            _animator = animator;
            this.enemyInfo = enemyInfo;
            _enemyTransform = enemyTransform;
            _runSpeed = runSpeed;
            _playerDetect = playerDetect;
            _rigidbody2D = rigidbody2D;
        }

        public override void Enter()
        {
            _enemyEventHandler.OnDestroyed += OnDestroy;
            _enemyEventHandler.OnAppliedDamage += OnDamaged;
            _animator.SetBool("Move",true);
        }

        public override void Exit()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _enemyEventHandler.OnDestroyed -= OnDestroy;
            _enemyEventHandler.OnAppliedDamage -= OnDamaged;
            _animator.SetBool("Move",false);
        }

        public override void FixedUpdate()
        {
            if (_playerDetect.closePlayer == null)
            {
                _switcher.Switch<EnemyIdleState>();
            }
            if (enemyInfo.isOnCliff)
            {
                _switcher.Switch<EnemyIdleState>();
            }

            float dir = 0;
            if (_playerDetect.closePlayer != null) dir = _playerDetect.closePlayer.transform.position.x - _enemyTransform.position.x;
            else _switcher.Switch<EnemyIdleState>();
            
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

            if (_playerDetect.closePlayer != null)
            {
                if (Vector2.Distance(_playerDetect.closePlayer.transform.position, _enemyTransform.position) < _attackDistance)
                {
                    _switcher.Switch<EnemyAttackState>();
                }
            }
            else _switcher.Switch<EnemyIdleState>();

            _rigidbody2D.velocity = new Vector2(dir * _runSpeed,_rigidbody2D.velocity.y);
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