using UnityEngine;

namespace Game.Script.Character
{
    public class PlayerNoneAttackState : StateBase
    {
        private PlayerStats _playerStats;
        private PlayerEventHandler playerEventHandler;
        public PlayerNoneAttackState(IStateSwitcher switcher,PlayerEventHandler playerEventHandler,
            PlayerStats playerStats) : base(switcher)
        {
            _playerStats = playerStats;
            this.playerEventHandler = playerEventHandler;
        }

        private void Attack()
        {
            if (_playerStats.isInvulnerable) return;
            _switcher.Switch<PlayerAttackState>();
        }

        public override void Enter()
        {
            playerEventHandler.OnCharacterDestroyed += OnDestroy;
            playerEventHandler.OnAttacked += Attack;
        }

        public override void Exit()
        {
            playerEventHandler.OnCharacterDestroyed -= OnDestroy;
            playerEventHandler.OnAttacked -= Attack;
        }

        public override void FixedUpdate()
        {
        }

        public override void Update()
        {
        }

        private void OnDestroy()
        {
            playerEventHandler.OnCharacterDestroyed -= OnDestroy;
            playerEventHandler.OnAttacked -= Attack;
        }
    }
}