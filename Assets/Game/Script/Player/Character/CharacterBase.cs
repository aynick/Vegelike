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
    protected PlayerInfo PlayerInfo;
    public PlayerStats playerStats;
    [SerializeField] protected PlayerStatsData playerStatsData;


    public virtual void GetDamage()
    {
        attackState.canAttack = true;
    }

    public virtual void Enable()
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
