using UnityEngine;

namespace Game.Script
{
    public class EnemyDamagedState : StateBase
    {
        private Rigidbody2D _rigidbody2D;
        private float _stanTime;
        private float time;
        private Animator _animator;
        
        public EnemyDamagedState(IStateSwitcher switcher,Rigidbody2D rigidbody2D,float stanTime,Animator animator) : base(switcher)
        {
            _animator = animator;
            _stanTime = stanTime;
            _rigidbody2D = rigidbody2D;
        }

        public override void Enter()
        {
            _animator.SetTrigger("Hurt");
            time = _stanTime;
            _rigidbody2D.velocity = Vector2.zero;
        }

        public override void Exit()
        {
        }

        public override void FixedUpdate()
        {
        }

        public override void Update()
        {
            time -= Time.deltaTime;
            if (time <= 0) _switcher.Switch<EnemyPatrolState>();
        }
    }
}