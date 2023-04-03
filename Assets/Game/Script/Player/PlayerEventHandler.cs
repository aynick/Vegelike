using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Script
{
    public class PlayerEventHandler : MonoBehaviour
    {
        public event Action OnDashed;
        public event Action OnAttacked;
        public event Action OnInteracted;
        public event Action<CharacterBase> OnNewCharacterChanged;
        public event Action OnCharacterChanged;
        public event Action OnCharacterDestroyed;
        
        //
        
        public event Action<int> OnAppliedDamage;
        public event Action<int> OnHealthPointChanged;

        [SerializeField] private Button dashBtn;
        [SerializeField] private Button attackBtn;
        [SerializeField] private Button interactBtn;
        [SerializeField] private Button characterChangeBtn;
        
        private void Start()
        {
            InitInput();
            dashBtn.onClick.AddListener(OnDash);
            attackBtn.onClick.AddListener(OnAttack);
            interactBtn.onClick.AddListener(OnInteract);
            characterChangeBtn.onClick.AddListener(OnCharacterChange);
        }

        private void OnDisable()
        {
            dashBtn.onClick.RemoveListener(OnDash);
            attackBtn.onClick.RemoveListener(OnAttack);
            interactBtn.onClick.RemoveListener(OnInteract);
        }

        public void OnCharacterDestroy()
        {
            OnCharacterDestroyed?.Invoke();
        }

        public void OnHealthPointChange(int hp)
        {
            OnHealthPointChanged?.Invoke(hp);
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
        }
    }
}