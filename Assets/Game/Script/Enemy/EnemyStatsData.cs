using UnityEngine;

namespace Game.Script
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Unit/Enemy", order = 0)]
    public class EnemyStatsData : ScriptableObject
    {
        public int damage;
        public float attackRange;
        public float attackRate;
        public float runSpeed;
        public float walkSpeed;
    }
}