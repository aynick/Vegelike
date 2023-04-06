using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Script
{
    public class CanvasRender : MonoBehaviour
    {
        private PlayerEventHandler playerEventHandler;
        [SerializeField] private Slider healthPoint;

        private void Start()
        {
            playerEventHandler = FindObjectOfType<PlayerEventHandler>();
            playerEventHandler.OnHealthPointChanged += RenderHealthPoint;
            Init();
        }
        
        private void Init()
        {
            var playerBeh = FindObjectOfType<PlayerBehavior>();
        }

        private void OnDisable()
        {
            playerEventHandler.OnHealthPointChanged -= RenderHealthPoint;
        }

        private void RenderHealthPoint(int hp,int maxHp)
        {
            healthPoint.maxValue = maxHp;
            healthPoint.value = hp;
        }
    }
}