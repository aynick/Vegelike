using System;
using System.Collections.Generic;
using Game.Script.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Script
{
    public class PlayerEventHandler : MonoBehaviour
    {
        public event Action OnDashed;
        public event Action OnAttacked;
        public event Action OnSkillUsed;
        public event Action OnInteracted;
        public event Action<CharacterBase> OnNewCharacterChanged;
        public event Action OnCharacterChanged;
        public event Action OnCharacterDestroyed;
        
        //
        
        public event Action<int> OnAppliedDamage;
        public event Action<int,int> OnHealthPointChanged;

        public event Action <int> OnRemovedItem;
        public event Action <Item[]> OnRenderedSlots;

        public event Action OnDisabled;

        [SerializeField] private Button dashBtn;
        [SerializeField] private Button attackBtn;
        [SerializeField] private Button interactBtn;
        [SerializeField] private Button characterChangeBtn;
        [SerializeField] private Button skillBtn;
        
        private void Start()
        {
            InitInput();
            dashBtn.onClick.AddListener(OnDash);
            attackBtn.onClick.AddListener(OnAttack);
            interactBtn.onClick.AddListener(OnInteract);
            characterChangeBtn.onClick.AddListener(OnCharacterChange);
            skillBtn.onClick.AddListener(OnSkillUse);
        }

        private void OnDisable()
        {
            OnDisabled?.Invoke();
            dashBtn.onClick.RemoveListener(OnDash);
            attackBtn.onClick.RemoveListener(OnAttack);
            interactBtn.onClick.RemoveListener(OnInteract);
            skillBtn.onClick.RemoveListener(OnSkillUse);
        }

        public void OnRemoveItem(int index)
        {
            OnRemovedItem?.Invoke(index);
        } 
        
        public void OnRenderSlots(Item[] items)
        {
            OnRenderedSlots?.Invoke(items);
        }

        private void OnSkillUse()
        {
            OnSkillUsed?.Invoke();
        }

        public void OnCharacterDestroy()
        {
            OnCharacterDestroyed?.Invoke();
        }

        public void OnHealthPointChange(int hp,int maxHp)
        {
            OnHealthPointChanged?.Invoke(hp,maxHp);
        }

        public void OnApplyDamage(int damage)
        {
            OnAppliedDamage?.Invoke(damage);
        }

        public void OnNewCharacterChange(CharacterBase character)
        {
            OnNewCharacterChanged?.Invoke(character);
        }
        private void OnCharacterChange()
        {
            OnCharacterChanged?.Invoke();
        }

        private void OnDash()
        {
            OnDashed?.Invoke();
        }
        
        private void OnAttack()
        {
            OnAttacked?.Invoke();
        }
        
        private void OnInteract()
        {
            OnInteracted?.Invoke();
        }

        private void InitInput()
        {
            var canvas = FindObjectOfType<PlayerCanvas>();
            characterChangeBtn = canvas._changeCharacterBtn;
            dashBtn = canvas._jumpBtn;
            attackBtn = canvas._attackBtn;
            interactBtn = canvas._interactBtn;
            skillBtn = canvas._skillBtn;
        }
    }
}