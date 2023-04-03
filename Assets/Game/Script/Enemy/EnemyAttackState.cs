using UnityEngine;

namespace Game.Script
{
    public class EnemyAttackState : StateBase
    {
        private EnemyPlayerDetect _playerDetect;
        private float _attackRate;
        private float _attackTime = 0;
        private int _damage;
        private float _attackRange;
        private Animator _animator;
        private Transform _enemyTransform;
        
        public EnemyAttackState(IStateSwitcher switcher,EnemyPlayerDetect playerDetect,float attackRate,int damage,
            float attackRange,Animator animator,Transform enemyTransform) : base(switcher)
        {
            _enemyTransform = enemyTransform;
            _animator = animator;
            _attackRange = attackRange;
            _attackRate = attackRate;
            _damage = damage;
            _playerDetect = playerDetect;
        }

        public override void Enter()
        {
            _attackTime = _attackRate;
        }

        public override void Exit()
        {
            _animator.SetBool("Attack",false);
        }

        public override void FixedUpdate()
        {
            
        }

        public override void Update()
        {
            if (_playerDetect.closePlayer != null)
            {
                _attackTime -= Time.deltaTime;
                var distance = Vector2.Distance(_enemyTransform.position,
                    _playerDetect.closePlayer.transform.position);
                if (_attackTime <= 0 && distance <= 2)
                {
                    Attack();
                }

                if (distance > 2)
                {
                    _switcher.Switch<EnemyChaseState>();
                }
            }
            else
            {
                _switcher.Switch<EnemyIdleState>();
            }
        }

        private void Attack()
        {
            _playerDetect.closePlayer.gameObject.TryGetComponent(out PlayerDamagable playerDamagable);
            if (playerDamagable != null)
            {
                playerDamagable.ApplyDamage(_damage);
                _animator.SetTrigger("Attack");
                _attackTime = _attackRate;
            }
        }
    }
}