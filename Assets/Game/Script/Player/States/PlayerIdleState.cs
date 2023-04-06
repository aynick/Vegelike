using Game.Script.Character;
using UnityEngine;

namespace Game.Script.Player.States
{
    public class PlayerIdleState : StateBase
    {
        private Animator _animator;
        private PlayerEventHandler _playerEventHandler;
        private Joystick _joystick;
        private PlayerInfo playerInfo;
        private Rigidbody2D _rigidbody2D;
        public PlayerIdleState(IStateSwitcher switcher,PlayerEventHandler playerEventHandler,Animator animator,
            Joystick joystick,PlayerInfo playerInfo,Rigidbody2D rigidbody2D) : base(switcher)
        {
            _rigidbody2D = rigidbody2D;
            this.playerInfo = playerInfo;
            _animator = animator;
            _playerEventHandler = playerEventHandler;
            _joystick = joystick;
        }

        public override void Enter()
        {
            _playerEventHandler.OnDisabled += OnDisable;
            _playerEventHandler.OnAttacked += Attack;
            _playerEventHandler.OnDashed += Jump;
            _playerEventHandler.OnAppliedDamage += OnDamaged;
            _animator.SetBool("Idle",true);
        }

        public override void Exit()
        {
            _playerEventHandler.OnDisabled -= OnDisable;
            _playerEventHandler.OnAppliedDamage -= OnDamaged;
            _playerEventHandler.OnDashed -= Jump;
            _playerEventHandler.OnAttacked -= Attack;
            _animator.SetBool("Idle",false);
        }

        public override void FixedUpdate()
        {
        }

        public override void Update()
        {
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            if (_joystick.Direction.normalized != Vector2.zero || !playerInfo.isGround)
            {
                _switcher.Switch<PlayerMoveState>();
            }
        }

        private void Jump()
        {
            _switcher.Switch<PlayerMoveState>();
        }

        private void Attack()
        {
            _switcher.Switch<PlayerNoneMoveState>();
        }
        
        private void OnDamaged(int damage)
        {
            _switcher.Switch<PlayerDamagedState>();
        }
        private void OnDisable()
        {
            _playerEventHandler.OnDisabled -= OnDisable;
            _playerEventHandler.OnAttacked -= Attack;
            _playerEventHandler.OnAppliedDamage -= OnDamaged;
            _playerEventHandler.OnDashed -= Jump;
        }
    }
}