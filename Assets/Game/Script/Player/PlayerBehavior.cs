using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Script;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour ,IStateSwitcher
{
    private List<StateBase> allStates;
    private StateBase currentState;

    private PlayerAttackState attackState;
    
    [SerializeField] private int moveSpeed;
    [SerializeField] private int jumpForce;
    [SerializeField] private int attackMaxCount;
    [SerializeField] private float attackRate;
    
    [SerializeField] private PlayerEventHandler playerEventHandler;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody2D;
    private Joystick joystick;
    [SerializeField] private Transform playerBody;
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask attackLayer;
    
    private void Start()
    {
        InitInput();
        attackState = new PlayerAttackState(this, attackMaxCount, animator, attackRate, rigidbody2D, attackPoint,
            attackRadius, attackLayer, playerEventHandler, moveSpeed, joystick,transform);
        allStates = new List<StateBase>()
        {
            new PlayerMoveState(this,joystick,rigidbody2D,animator,moveSpeed,playerBody,jumpForce,
                playerEventHandler,playerStats),
            new PlayerIdleState(this,playerEventHandler,animator,joystick,playerStats,rigidbody2D),
            attackState
        };
        currentState = allStates[0];
        currentState.Enter();
    }

    private void InitInput()
    {
        joystick = FindObjectOfType<PlayerCanvas>()._walkJoystick;
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
        
        if (joystick.Horizontal > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, 
                transform.localScale.y);
        }
        if (joystick.Horizontal < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y,
                transform.localScale.z);
        }
    }

    private void Update()
    {
        currentState.Update();
    }

    public void GetDamage()
    {
        attackState.canAttack = true;
    }

    public void Switch<T>() where T : StateBase
    {
        var state = allStates.FirstOrDefault(state => state is T);
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position,attackRadius);
    }
}
