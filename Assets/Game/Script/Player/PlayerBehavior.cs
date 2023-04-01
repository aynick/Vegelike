using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Script;
using Game.Script.Character;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour ,IStateSwitcher
{
    private List<StateBase> allStates;
    private StateBase currentState;

    [SerializeField] public int moveSpeed;
    [SerializeField] private int jumpForce;
    
    
    [SerializeField] public PlayerEventHandler playerEventHandler;
    [SerializeField] public Animator animator;
    [SerializeField] public Rigidbody2D rigidbody2D;
    public Joystick joystick;
    [SerializeField] private Transform playerBody;
    [SerializeField] public PlayerStats playerStats;

    
    
    private void Start()
    {
        playerEventHandler.OnNewCharacterChanged += ChangeVars;
        InitInput();
        InitStates();
    }

    private void InitInput()
    {
        joystick = FindObjectOfType<PlayerCanvas>()._walkJoystick;
    }

    private void ChangeVars(CharacterBase characterBase)
    {
        animator = characterBase.animator;
        InitStates();
    }

    private void InitStates()
    {
        allStates = new List<StateBase>()
        {
            new PlayerMoveState(this,joystick,rigidbody2D,animator,moveSpeed,playerBody,jumpForce,
                playerEventHandler,playerStats),
            new PlayerIdleState(this,playerEventHandler,animator,joystick,playerStats,rigidbody2D),
            new PlayerNoneMoveState(this,playerEventHandler,playerStats)
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

    public void Switch<T>() where T : StateBase
    {
        var state = allStates.FirstOrDefault(state => state is T);
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    
}
