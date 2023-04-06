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
        [SerializeField] private EnemyInfo enemyInfo;
        [SerializeField] private float attackRate;
        [SerializeField] private float attackRange;
        [SerializeField] private int attackDamage;
        [SerializeField] private Animator animator;
        [SerializeField] private EnemyEventHandler enemyEventHandler;
        [SerializeField] private float stanTime;
        [SerializeField] private int healthPoint;

        private void OnEnable()
        {
            enemyEventHandler.OnAppliedDamage += ApplyDamage;
            InitStates();
        }

        private void OnDisable()
        {
            enemyEventHandler.OnAppliedDamage -= ApplyDamage;
        }

        private void ApplyDamage(int damage)
        {
            if ((healthPoint - damage) <= 0)
            {
                enemyEventHandler.OnEnemyDestroy();
                Destroy(gameObject);
                return;
            }
            healthPoint -= damage;
        }

        private void InitStates()
        {
            allStates = new List<StateBase>()
            {
                new EnemyIdleState(this,_enemyPlayerDetect,_rigidbody2D,enemyInfo,animator,enemyEventHandler),
                new EnemyChaseState(this,_enemyPlayerDetect,_rigidbody2D,_runSpeed,transform,enemyInfo,animator,enemyEventHandler),
                new EnemyPatrolState(this,_rigidbody2D,_walkSpeed,enemyInfo,transform,_enemyPlayerDetect,animator,enemyEventHandler),
                new EnemyAttackState(this,_enemyPlayerDetect,attackRate,attackDamage,attackRange,animator,transform,enemyEventHandler),
                new EnemyDamagedState(this,_rigidbody2D,stanTime,animator)
            };
            currentState = allStates[0];
            currentState.Enter();
        }

        private void InitVars()
        {
            
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        private void Update()
        {
            currentState.Update();
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
