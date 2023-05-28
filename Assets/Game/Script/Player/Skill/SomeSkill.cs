using System;
using Game.Script.Player.States;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Script
{
    public class SomeSkill : PlayerSkillBase
    {
        private Animator animator;
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform projectileCastPoint;
        [SerializeField] private AttackType attackType;
        [SerializeField] private int damage;
        [SerializeField] private float coolDown; 
        private float time;
        private bool canCast;
        private PlayerEventHandler playerEventHandler;
        private CharacterBase characterBase;

        private void Start()
        {
            characterBase = GetComponent<CharacterBase>();
            transform.parent.TryGetComponent(out PlayerBehavior playerBehavior);
            playerEventHandler = playerBehavior.playerEventHandler;
            animator = GetComponent<Animator>();
            playerEventHandler.OnSkillUsed += Use;
        }

        private void OnDisable()
        {
            playerEventHandler.OnSkillUsed -= Use;
        }

        public override void Use()
        {
            if (!canCast) return;
            characterBase.Switch<PlayerUseSkillState>();
        }

        public void CastProjectile()
        {
            time = coolDown;
            canCast = false;
            var newProjectile = Instantiate(projectile,projectileCastPoint.position,quaternion.identity);
            newProjectile.TryGetComponent(out ProjectileBase projectileBase);
            projectileBase.horizontalFlip = Mathf.Clamp((int)transform.parent.transform.localScale.x,-1,1);
            projectileBase.dir = new Vector2(transform.parent.transform.localScale.x,0);
            projectileBase.TryGetComponent(out AttackProjectile attackProjectile);
            attackProjectile.damage = damage;
        }

        private void FixedUpdate()
        {
            if (time <= 0)
            {
                canCast = true;
                return;
            }
            time -= Time.fixedDeltaTime;
        }

        public override void Enter()
        {
            damage = characterBase.playerStats.damage;
            animator.SetTrigger("Skill");
        }

        public override void Exit()
        {
            
        }

        public override void Loop()
        {
        }
    }
}