using Game.Script.Character;
using Game.Script.Player.States;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace Game.Script
{
    public class PlayerMoveState : StateBase
    {
        private Joystick _joystick;
        private Rigidbody2D _rigidbody2D;
        private int _moveSpeed;
        private Animator _animator;
        private Transform transform;
        private int _jumpForce;
        private PlayerEventHandler _playerEventHandler;
        private PlayerInfo playerInfo;
        
        public PlayerMoveState(IStateSwitcher switcher,Joystick joystick,Rigidbody2D rigidbody2D,Animator animator,
            int speed,Transform player,int jumpForce,PlayerEventHandler playerEventHandler,PlayerInfo playerInfo) : base(switcher)
        {
            this.playerInfo = playerInfo;
            _playerEventHandler = playerEventHandler;
            _jumpForce = jumpForce;
            transform = player;
            _moveSpeed = speed;
            _joystick = joystick;
            _rigidbody2D = rigidbody2D;
            _animator = animator;
            _playerEventHandler.OnDashed += Jump;
        }

        public override void Enter()
        {
            _playerEventHandler.OnDisabled += OnDisable;
            _playerEventHandler.OnAttacked += Attack;
            _playerEventHandler.OnAppliedDamage += OnDamaged;
            _animator.SetBool("Move",true);
        }

        public override void Exit()
        {
            _playerEventHandler.OnDisabled -= OnDisable;
            _playerEventHandler.OnAttacked -= Attack;
            _playerEventHandler.OnAppliedDamage -= OnDamaged;
            _animator.SetBool("Jump",false);
            _animator.SetBool("Move",false);
        }

        public override void FixedUpdate()
        {
            _rigidbody2D.velocity = new Vector2(_joystick.Horizontal * _moveSpeed, _rigidbody2D.velocity.y);
            if (_joystick.Horizontal > 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, 
                    transform.localScale.y);
            }
            if (_joystick.Horizontal < 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y,
                    transform.localScale.z);
            }
        }

        public override void Update()
        {
            if (playerInfo.isGround && _joystick.Direction.normalized == Vector2.zero)
            {
                _switcher.Switch<PlayerIdleState>();
            }
            if (!playerInfo.isGround)
            {
                _animator.SetBool("Jump", true);
                _animator.SetBool("Move",false);
            }
            if (playerInfo.isGround && _joystick.Direction.normalized != Vector2.zero)
            {
                _animator.SetBool("Jump", false);
                _animator.SetBool("Move",true);
            }
        }

        private void Jump()
        {
            if (playerInfo.isGround)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
            }
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
            _playerEventHandler.OnAttacked -= Attack;
            _playerEventHandler.OnAppliedDamage -= OnDamaged;
            _playerEventHandler.OnDashed -= Jump;
        }
    }
}