using UnityEngine;

namespace Game.Script.Character
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
            
        }

        public override void Exit()
        {
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
    }
}