using Game.Script.Character;
using UnityEngine;

namespace Game.Script.Player.States
{
    public class PlayerUseSkillState : StateBase
    {
        private PlayerSkillBase _skillBase;
        private float time;
        private PlayerInfo _playerInfo;
        public PlayerUseSkillState(IStateSwitcher switcher,PlayerSkillBase skillBase,PlayerInfo playerInfo) : base(switcher)
        {
            _playerInfo = playerInfo;
            _skillBase = skillBase;
        }

        public override void Enter()
        {
            _playerInfo.canMove = false;
            time = _skillBase.skillTime;
            _skillBase.Enter();
        }

        public override void Exit()
        {
            _playerInfo.canMove = true;
            _skillBase.Exit();
        }

        public override void FixedUpdate()
        {
            time -= Time.fixedDeltaTime;
            if (time <= 0)
            {
                _switcher.Switch<PlayerNoneAttackState>();
            }
            _skillBase.Loop();
        }

        public override void Update()
        {
        }
    }
}