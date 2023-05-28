using UnityEngine;

namespace Game.Script
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Player", order = 0)]
    public class PlayerStatsData : ScriptableObject
    {
        public int moveSpeed;
        public int damage;
        public int jumpForce;
        public float playerStanTime;
        public int attackMaxCount;
        public int knockbackForce;
        public float attackRate;
        public float attackRadius;
        public float comboRate;
    }
}