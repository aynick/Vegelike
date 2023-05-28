using System;
using System.Collections.Generic;
using System.Linq;
using Game.Script.Player.States;
using UnityEngine;

namespace Game.Script.Character
{
    public class CarrotCharacter : CharacterBase
    {
        private void Start()
        {
            Enable();
        }

        public override void Enable()
        {
            InitVars();
            InitStates();
        }

        private void InitStates()
        {
            attackState = new PlayerAttackState(this,playerStats.attackMaxCount,animator,playerStats.attackRate,rigidbody2D,attackPoint,playerStats.attackRadius,
                attackLayer,playerEventHandler,moveSpeed,joystick,transform,PlayerInfo,playerStats.damage,playerStats.knockbackForce,playerStats.comboRate);
            allStates = new List<StateBase>()
            {
                new PlayerUseSkillState(this,skillBase,PlayerInfo),
                new PlayerNoneAttackState(this,playerEventHandler,PlayerInfo),
                attackState
            };
            SetStateByDefault();
        }
        private void SetStateByDefault()
        {
            var state = allStates.FirstOrDefault(state => state is PlayerNoneAttackState);
            currentState = state;
            currentState.Enter();
        }

        private void InitVars()
        {
            _playerBehavior = transform.parent?.GetComponent<PlayerBehavior>();
            playerEventHandler = _playerBehavior.playerEventHandler;
            playerStats = new PlayerStats(playerStatsData,playerEventHandler);
            animator = GetComponent<Animator>();
            PlayerInfo = _playerBehavior.playerInfo;
            
            _playerBehavior.animator = animator;
            rigidbody2D = _playerBehavior.rigidbody2D;
            joystick = _playerBehavior.joystick;
            
            playerEventHandler.OnNewCharacterChange(this);
            Debug.Log("vars inited");
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
            Gizmos.DrawWireSphere(attackPoint.position,playerStatsData.attackRadius);
        }
    }
}