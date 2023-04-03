using Game.Script;
using UnityEngine;

public class PlayerDamagable : MonoBehaviour
{
    [SerializeField] private PlayerEventHandler playerEventHandler;
    [SerializeField] private PlayerStats playerStats;

    public void ApplyDamage(int damage)
    {
        if (playerStats.isInvulnerable) return; 
        playerEventHandler.OnApplyDamage(damage);
    }
}
