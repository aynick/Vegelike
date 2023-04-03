using UnityEngine;

namespace Game.Script.Player.States
{
    public class PlayerNoneMoveState : StateBase
    {
        private PlayerEventHandler playerEventHandler;
        private PlayerStats _playerStats;
        
        public PlayerNoneMoveState(IStateSwitcher switcher,PlayerEventHandler playerEventHandler,PlayerStats playerStats) : base(switcher)
        {
            _playerStats = playerStats;
            this.playerEventHandler = playerEventHandler;
            this.playerEventHandler.OnDashed += Jump;
        }

        public override void Enter()
        {
            playerEventHandler.OnAppliedDamage += OnDamaged;
        }

        public override void Exit()
        {
            playerEventHandler.OnAppliedDamage -= OnDamaged;
        }

        public override void FixedUpdate()
        {
            if (_playerStats.canMove)
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
    }
}