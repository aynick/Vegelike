using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagable : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void ApplyDamage()
    {
        Debug.Log("Apply");
        animator.SetTrigger("Hurt");
    }
}
