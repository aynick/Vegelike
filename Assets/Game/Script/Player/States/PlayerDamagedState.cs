using UnityEngine;

namespace Game.Script.Player.States
{
    public class PlayerDamagedState : StateBase
    {
        private PlayerStats _playerStats;
        private float _stanTime;
        private float _time;
        private Animator _animator;
        
        public PlayerDamagedState(IStateSwitcher switcher,float stanTime,PlayerStats playerStats,
            Animator animator) : base(switcher)
        {
            _animator = animator;
            _playerStats = playerStats;
            _stanTime = stanTime;
        }

        public override void Enter()
        {
            _time = _stanTime;
            _playerStats.isInvulnerable = true;
            _animator.SetTrigger("Hurt");
        }

        public override void Exit()
        {
            _playerStats.isInvulnerable = false;
        }

        public override void FixedUpdate()
        {
            _time -= Time.fixedDeltaTime;
            if (_time <= 0)
            {
                _switcher.Switch<PlayerIdleState>();
            }
        }

        public override void Update()
        {
        }
    }
}