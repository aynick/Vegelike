using System;
using UnityEngine;

namespace Game.Script
{
    public class EnemyEventHandler : MonoBehaviour
    {
        public event Action<int> OnAppliedDamage;
        public event Action OnDestroyed;

        public void OnEnemyDestroy()
        {
            OnDestroyed?.Invoke();
        }

        public void OnApplyDamage(int damage)
        {
            OnAppliedDamage?.Invoke(damage);
        }
    }
    
}