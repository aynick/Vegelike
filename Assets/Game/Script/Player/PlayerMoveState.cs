﻿using UnityEngine;
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
        private PlayerStats _playerStats;
        
        public PlayerMoveState(IStateSwitcher switcher,Joystick joystick,Rigidbody2D rigidbody2D,Animator animator,
            int speed,Transform player,int jumpForce,PlayerEventHandler playerEventHandler,PlayerStats playerStats) : base(switcher)
        {
            _playerStats = playerStats;
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
            _playerEventHandler.OnAttacked += Attack;
            _animator.SetBool("Move",true);
        }

        public override void Exit()
        {
            _playerEventHandler.OnAttacked -= Attack;
            _animator.SetBool("Jump",false);
            _animator.SetBool("Move",false);
        }

        public override void FixedUpdate()
        {
            _rigidbody2D.velocity = new Vector2(_joystick.Horizontal * _moveSpeed, _rigidbody2D.velocity.y);
        }

        public override void Update()
        {
            if (_playerStats.isGround && _joystick.Direction.normalized == Vector2.zero)
            {
                _switcher.Switch<PlayerIdleState>();
            }
            if (!_playerStats.isGround)
            {
                _animator.SetBool("Jump", true);
                _animator.SetBool("Move",false);
            }
            if (_playerStats.isGround && _joystick.Direction.normalized != Vector2.zero)
            {
                _animator.SetBool("Jump", false);
                _animator.SetBool("Move",true);
            }
        }

        private void Jump()
        {
            if (_playerStats.isGround)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
            }
        }
        
        private void Attack()
        {
            _switcher.Switch<PlayerAttackState>();
        }
    }
}