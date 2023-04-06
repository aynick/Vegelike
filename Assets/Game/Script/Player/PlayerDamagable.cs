using Game.Script;
using UnityEngine;

public class PlayerDamagable : MonoBehaviour
{
    [SerializeField] private PlayerEventHandler playerEventHandler;
    [SerializeField] private PlayerInfo playerInfo;

    public void ApplyDamage(int damage)
    {
        if (playerInfo.isInvulnerable) return; 
        playerEventHandler.OnApplyDamage(damage);
    }
}
