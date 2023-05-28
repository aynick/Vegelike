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
    private PlayerMoveState moveState;

    [SerializeField] public PlayerEventHandler playerEventHandler;
    [SerializeField] public Animator animator;
    [SerializeField] public Rigidbody2D rigidbody2D;
    public Joystick joystick { private set; get; }
    [SerializeField] private Transform playerBody;
    [SerializeField] public PlayerInfo playerInfo;

  
    public PlayerStats playerStats;

    [SerializeField] private AudioSource stepSound;

    [SerializeField] private PlayerStatsData playerStatsData;
    
    private void Start()
    {
        InitVars();
        playerEventHandler.OnNewCharacterChanged += ChangeVars;
        InitInput();
    }

    private void InitVars()
    {
      
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
        moveState = new PlayerMoveState(this, joystick, rigidbody2D, animator, playerStats.moveSpeed, playerBody,
            playerStats.jumpForce,
            playerEventHandler, playerInfo);
        allStates = new List<StateBase>()
        {
            moveState,
            new PlayerIdleState(this,playerEventHandler,animator,joystick,playerInfo,rigidbody2D),
            new PlayerNoneMoveState(this,playerEventHandler,playerInfo),
            new PlayerDamagedState(this,playerStats.playerStanTime,playerInfo,animator,rigidbody2D)
        };
        SetStateByDefault();
    }

    private void SetStateByDefault()
    {
        var state = allStates.FirstOrDefault(state => state is PlayerIdleState);
        if (currentState != null) currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
    
   

    public void PlaySound(AudioSource audioSource)
    {
        audioSource.Play();
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
