using UnityEngine;

namespace Game.Script.Player.States
{
    public class PlayerDamagedState : StateBase
    {
        private PlayerInfo playerInfo;
        private float _stanTime;
        private float _time;
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        
        public PlayerDamagedState(IStateSwitcher switcher,float stanTime,PlayerInfo playerInfo,
            Animator animator,Rigidbody2D rigidbody2D) : base(switcher)
        {
            _rigidbody2D = rigidbody2D;
            _animator = animator;
            this.playerInfo = playerInfo;
            _stanTime = stanTime;
        }

        public override void Enter()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _time = _stanTime;
            playerInfo.isInvulnerable = true;
            _animator.SetTrigger("Hurt");
        }

        public override void Exit()
        {
            playerInfo.isInvulnerable = false;
        }

        public override void FixedUpdate()
        {
            _time -= Time.fixedDeltaTime;
            if (_time <= 0)
            {
                _switcher.Switch<PlayerIdleState>();
            }
        }

        public override void Update()
        {
        }
    }
}