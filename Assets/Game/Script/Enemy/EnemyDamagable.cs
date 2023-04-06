using System;
using System.Collections;
using System.Collections.Generic;
using Game.Script;
using UnityEngine;

public class EnemyDamagable : MonoBehaviour
{
    [SerializeField] private EnemyEventHandler enemyEventHandler;
    [SerializeField] private Animator animator;

    public void ApplyDamage(int damage)
    {
        enemyEventHandler.OnApplyDamage(damage);
    }
}
