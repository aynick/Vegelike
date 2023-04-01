using UnityEngine;

namespace Game.Script.Character
{
    public class PlayerNoneAttackState : StateBase
    {
        private PlayerEventHandler playerEventHandler;
        public PlayerNoneAttackState(IStateSwitcher switcher,PlayerEventHandler playerEventHandler) : base(switcher)
        {
            this.playerEventHandler = playerEventHandler;
        }

        private void Attack()
        {
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