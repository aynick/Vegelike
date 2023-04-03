using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Script.Character
{
    public class CarrotCharacter : CharacterBase
    {
        [SerializeField] private int attackMaxCount;
        [SerializeField] private float attackRate;
        
        private void Start()
        {
            _playerBehavior = transform.parent?.GetComponent<PlayerBehavior>();
            InitVars();
            InitStates();
        }

        private void InitStates()
        {
            attackState = new PlayerAttackState(this,attackMaxCount,animator,attackRate,rigidbody2D,attackPoint,attackRadius,
                attackLayer,playerEventHandler,moveSpeed,joystick,transform,playerStats);
            allStates = new List<StateBase>()
            {
                new PlayerNoneAttackState(this,playerEventHandler,playerStats),
                attackState
            };
            currentState = allStates[0];
            currentState.Enter();
        }

        private void InitVars()
        {
            animator = GetComponent<Animator>();
            playerStats = _playerBehavior.playerStats;
            _playerBehavior.animator = animator;
            rigidbody2D = _playerBehavior.rigidbody2D;
            moveSpeed = _playerBehavior.moveSpeed;
            joystick = _playerBehavior.joystick;
            playerEventHandler = _playerBehavior.playerEventHandler;
            playerEventHandler.OnNewCharacterChange(this);
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        private void Update()
        {
            currentState.Update();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(attackPoint.position,attackRadius);
        }
    }
}