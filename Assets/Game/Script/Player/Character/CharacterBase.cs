using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    protected PlayerBehavior _playerBehavior;
    
    public virtual void GetDamage()
    {
        _playerBehavior.GetDamage();
    }
}
