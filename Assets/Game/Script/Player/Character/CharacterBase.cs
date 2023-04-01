using System;
using System.Collections.Generic;
using System.Linq;
using Game.Script;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IStateSwitcher
{
    protected PlayerAttackState attackState;
    protected List<StateBase> allStates;
    protected StateBase currentState;
    protected PlayerBehavior _playerBehavior;
    public Animator animator;
    protected Rigidbody2D rigidbody2D;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float attackRadius; 
    [SerializeField] protected LayerMask attackLayer;
    protected PlayerEventHandler playerEventHandler;
    protected int moveSpeed;
    protected Joystick joystick;
    protected PlayerStats playerStats;


    public virtual void GetDamage()
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
}
