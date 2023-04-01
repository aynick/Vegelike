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

        [SerializeField] private Button dashBtn;
        [SerializeField] private Button attackBtn;
        [SerializeField] private Button interactBtn;
        
        private void Start()
        {
            InitInput();
            dashBtn.onClick.AddListener(OnDash);
            attackBtn.onClick.AddListener(OnAttack);
            interactBtn.onClick.AddListener(OnInteract);
        }

        private void OnDisable()
        {
            dashBtn.onClick.RemoveListener(OnDash);
            attackBtn.onClick.RemoveListener(OnAttack);
            interactBtn.onClick.RemoveListener(OnInteract);
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
            dashBtn = FindObjectOfType<PlayerCanvas>()._jumpBtn;
            attackBtn = FindObjectOfType<PlayerCanvas>()._attackBtn;
            interactBtn = FindObjectOfType<PlayerCanvas>()._interactBtn;
        }
    }
}