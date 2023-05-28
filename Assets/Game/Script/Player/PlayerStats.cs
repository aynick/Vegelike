using UnityEngine;

namespace Game.Script
{
    public class PlayerStats
    {
        private PlayerEventHandler _playerEventHandler;
        private PlayerStatsData _playerStatsData;
        //stats
        public int moveSpeed { private set; get; }
        public int damage { private set; get; }
        public int jumpForce { private set; get; }
        public float playerStanTime { private set; get; }
        public float attackRate { private set; get; }
        public float attackRadius { private set; get; }
        public float comboRate { private set; get; }
        public int attackMaxCount { private set; get; }
        public int knockbackForce { private set; get; }

        public PlayerStats(PlayerStatsData playerStatsData,PlayerEventHandler playerEventHandler)
        {
            _playerStatsData = playerStatsData; 
            _playerEventHandler = playerEventHandler;
            OnEnable();
        }

        private void OnEnable()
        {
            _playerEventHandler.OnDisabled += OnDisable;
            _playerEventHandler.OnCharacterDestroyed += OnDisable;
            damage = _playerStatsData.damage;
            comboRate = _playerStatsData.comboRate;
            knockbackForce = _playerStatsData.knockbackForce;
            attackRadius = _playerStatsData.attackRadius;
            attackMaxCount = _playerStatsData.attackMaxCount;
            attackRate = _playerStatsData.attackRate;
            moveSpeed = _playerStatsData.moveSpeed;
            jumpForce = _playerStatsData.jumpForce;
            playerStanTime = _playerStatsData.playerStanTime;
        }

        private void OnDisable()
        {            
            _playerEventHandler.OnCharacterDestroyed -= OnDisable;
            _playerEventHandler.OnDisabled -= OnDisable;
        }
    }
}