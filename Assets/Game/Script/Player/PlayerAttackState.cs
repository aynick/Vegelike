using UnityEngine;

namespace Game.Script
{
    public class PlayerAttackState : StateBase
    {
        private int _attackCount;
        private int _attackMaxCount;
        private Animator _animator;
        private float _attackTime;
        private float _attackRate;
        private Rigidbody2D _rigidbody2D;
        private Transform _attackPoint;
        private float _attackRadius;
        private LayerMask _enemyLayer;
        private PlayerEventHandler _playerEventHandler;
        private Joystick _joystick;
        private Transform _playerTransform;
        
        private float _comboTime;
        private float _comboRate = 0.5f;
        
        private float _moveSpeed;

        public bool canAttack;
        
        public PlayerAttackState(IStateSwitcher switcher,int attackMaxCount,Animator animator,float attackRate,
            Rigidbody2D rigidbody2D,Transform attackPoint,float attackRadius,LayerMask enemyLayer,
            PlayerEventHandler playerEventHandler,float moveSpeed,Joystick joystick,
            Transform playerTransform) : base(switcher)
        {
            _playerTransform = playerTransform;
            _joystick = joystick;
            _moveSpeed = moveSpeed / 2;
            _playerEventHandler = playerEventHandler;
            _enemyLayer = enemyLayer;
            _attackPoint = attackPoint;
            _attackRadius = attackRadius;
            _rigidbody2D = rigidbody2D;
            _attackMaxCount = attackMaxCount;
            _animator = animator;
            _attackRate = attackRate;
        }

        public override void Enter()
        {
            Attack();
            _playerEventHandler.OnDashed += Jump;
            _playerEventHandler.OnAttacked += Attack;
        }

        public override void Exit()
        {
        }

        public override void FixedUpdate()
        {
            _attackTime -= Time.fixedDeltaTime;
            if (_attackTime <= 0)
            {
                _attackCount = 0;
                _switcher.Switch<PlayerIdleState>();
                _rigidbody2D.velocity = Vector2.zero;
            }

            _comboTime -= Time.fixedDeltaTime;
            if (canAttack)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius, _enemyLayer);
                foreach (var collider in colliders)
                {
                    collider.TryGetComponent(out EnemyDamagable enemyDamagable);
                    enemyDamagable.ApplyDamage();
                    enemyDamagable.TryGetComponent(out Rigidbody2D rigidbody2D);
                    rigidbody2D.velocity = new Vector2((enemyDamagable.transform.position - _playerTransform.position).normalized.x * 70,
                         rigidbody2D.velocity.y);
                }
                canAttack = false;
            }
        }

        public override void Update()
        {
        }

        private void Attack()
        {
            if (_comboTime <= 0)
            {
                _rigidbody2D.velocity = Vector2.right * (_joystick.Direction.normalized.x * _moveSpeed);
                Debug.Log(_rigidbody2D.velocity);
                if (_attackCount == _attackMaxCount) _attackCount = 0;
                _attackCount++;
                _animator.SetTrigger("Attack" + _attackCount);
                _attackTime = _attackRate;
                _comboTime = _comboRate;
            }
        }

        private void Jump()
        {
            _switcher.Switch<PlayerMoveState>();
        }
    }
}