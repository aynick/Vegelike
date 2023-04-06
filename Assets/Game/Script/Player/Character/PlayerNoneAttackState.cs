using UnityEngine;

namespace Game.Script.Character
{
    public class PlayerNoneAttackState : StateBase
    {
        private PlayerInfo _playerInfo;
        private PlayerEventHandler _playerEventHandler;
        public PlayerNoneAttackState(IStateSwitcher switcher,PlayerEventHandler playerEventHandler,
            PlayerInfo playerInfo) : base(switcher)
        {
            _playerInfo = playerInfo;
            _playerEventHandler = playerEventHandler;
        }

        private void Attack()
        {
            if (_playerInfo.isInvulnerable) return;
            _switcher.Switch<PlayerAttackState>();
        }

        public override void Enter()
        {
            _playerEventHandler.OnDisabled += OnDisable;
            _playerEventHandler.OnCharacterDestroyed += OnDestroy;
            _playerEventHandler.OnAttacked += Attack;
        }

        public override void Exit()
        {
            _playerEventHandler.OnDisabled -= OnDisable;
            _playerEventHandler.OnCharacterDestroyed -= OnDestroy;
            _playerEventHandler.OnAttacked -= Attack;
        }

        public override void FixedUpdate()
        {
        }

        public override void Update()
        {
        }

        private void OnDestroy()
        {
            _playerEventHandler.OnCharacterDestroyed -= OnDestroy;
            _playerEventHandler.OnAttacked -= Attack;
        }

        private void OnDisable()
        {
            _playerEventHandler.OnDisabled -= OnDisable;
            _playerEventHandler.OnCharacterDestroyed -= OnDestroy;
            _playerEventHandler.OnAttacked -= Attack;
        }
    }
}