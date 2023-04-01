using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Script
{ 
    public class EnemyBehavior : MonoBehaviour , IStateSwitcher
    {
        private List<StateBase> allStates;
        private StateBase currentState;

        [SerializeField] private EnemyPlayerDetect _enemyPlayerDetect;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private EnemyStats enemyStats;


        private void Start()
        {
            allStates = new List<StateBase>()
            {
                new EnemyIdleState(this,_enemyPlayerDetect,_rigidbody2D,enemyStats),
                new EnemyChaseState(this,_enemyPlayerDetect,_rigidbody2D,_runSpeed,transform,enemyStats),
                new EnemyPatrolState(this,_rigidbody2D,_walkSpeed,enemyStats,transform,_enemyPlayerDetect),
                new EnemyAttackState(this)
            };
            currentState = allStates[0];
            currentState.Enter();
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        private void Update()
        {
            currentState.Update();
        }

        private void Flip()
        {
            
        }

        public void Switch<T>() where T : StateBase
        {
            var state = allStates.FirstOrDefault(state => state is T);
            currentState.Exit();
            currentState = state;
            currentState.Enter();
        }
    }
}
