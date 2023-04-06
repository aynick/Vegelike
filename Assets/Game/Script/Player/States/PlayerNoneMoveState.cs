using UnityEngine;

namespace Game.Script.Player.States
{
    public class PlayerNoneMoveState : StateBase
    {
        private PlayerEventHandler _playerEventHandler;
        private PlayerInfo _playerInfo;
        
        public PlayerNoneMoveState(IStateSwitcher switcher,PlayerEventHandler playerEventHandler,PlayerInfo playerInfo) : base(switcher)
        {
            _playerInfo = playerInfo;
            _playerEventHandler = playerEventHandler;
        }

        public override void Enter()
        {
            _playerEventHandler.OnDashed += Jump;
            _playerEventHandler.OnAppliedDamage += OnDamaged;
        }

        public override void Exit()
        {
            _playerEventHandler.OnDashed -= Jump;
            _playerEventHandler.OnAppliedDamage -= OnDamaged;
        }

        public override void FixedUpdate()
        {
            if (_playerInfo.canMove)
            {
                _switcher.Switch<PlayerIdleState>();
            }
        }

        public override void Update()
        {
        }

        private void Jump()
        {
            _switcher.Switch<PlayerMoveState>();
        }
        
        private void OnDamaged(int damage)
        {
            _switcher.Switch<PlayerDamagedState>();
        }
        
        private void OnDisable()
        {
            _playerEventHandler.OnDisabled -= OnDisable;
            _playerEventHandler.OnAppliedDamage -= OnDamaged;
            _playerEventHandler.OnDashed -= Jump;
        }
    }
}