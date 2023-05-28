using UnityEngine;

namespace Game.Script
{
    public abstract class PlayerSkillBase : MonoBehaviour
    {
        [SerializeField] protected SkillType skillType;
        [SerializeField] public float skillTime;
        public abstract void Use();
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Loop();
    }
}