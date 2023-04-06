using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Script;
using Game.Script.Player.States;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour ,IStateSwitcher
{
    private List<StateBase> allStates;
    private StateBase currentState;

    [SerializeField] public PlayerEventHandler playerEventHandler;
    [SerializeField] public Animator animator;
    [SerializeField] public Rigidbody2D rigidbody2D;
    public Joystick joystick { private set; get; }
    [SerializeField] private Transform playerBody;
    [SerializeField] public PlayerInfo playerInfo;

    [SerializeField] public int maxHealthPoint;
    public int healthPoint;
    
    public PlayerStats playerStats;

    [SerializeField] private PlayerStatsData playerStatsData;
    
    private void Start()
    {
        InitVars();
        playerEventHandler.OnNewCharacterChanged += ChangeVars;
        playerEventHandler.OnAppliedDamage += ApplyDamage;
        InitInput();
    }

    private void InitVars()
    {
        healthPoint = maxHealthPoint;
        playerEventHandler.OnHealthPointChange(healthPoint,maxHealthPoint);
    }

    private void InitInput()
    {
        joystick = FindObjectOfType<PlayerCanvas>()._walkJoystick;
    }

    private void ChangeVars(CharacterBase characterBase)
    {
        playerStats = characterBase.playerStats;
        animator = characterBase.animator;
        InitStates();
    }

    private void InitStates()
    {
        allStates = new List<StateBase>()
        {
            new PlayerMoveState(this,joystick,rigidbody2D,animator,playerStats.moveSpeed,playerBody,playerStats.jumpForce,
                playerEventHandler,playerInfo),
            new PlayerIdleState(this,playerEventHandler,animator,joystick,playerInfo,rigidbody2D),
            new PlayerNoneMoveState(this,playerEventHandler,playerInfo),
            new PlayerDamagedState(this,playerStats.playerStanTime,playerInfo,animator,rigidbody2D)
        };
        if (currentState != null) currentState.Exit();
        currentState = allStates[0];
        currentState.Enter();
    }

    private void SetStateByDefault()
    {
        var state = allStates.FirstOrDefault(state => state is PlayerIdleState);
        currentState = state;
        currentState.Enter();
    }
    
    private void ApplyDamage(int damage)
    {
        if (healthPoint - damage <= 0)
        {
            healthPoint = 0;
            Die();
            playerEventHandler.OnHealthPointChange(healthPoint,maxHealthPoint);
            return;
        }
        healthPoint -= damage;
        playerEventHandler.OnHealthPointChange(healthPoint,maxHealthPoint);
    }

    private void Die()
    {
        
    }
    
    private void FixedUpdate()
    {
        if(currentState == null) return;
        currentState.FixedUpdate();
    }

    private void Update()
    {
        if(currentState == null) return;
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
