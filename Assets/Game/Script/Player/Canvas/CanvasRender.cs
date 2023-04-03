using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Script
{
    public class CanvasRender : MonoBehaviour
    {
        private PlayerEventHandler playerEventHandler;
        [SerializeField] private Slider healthPoint;

        private void OnEnable()
        {
            playerEventHandler = FindObjectOfType<PlayerEventHandler>();
            playerEventHandler.OnHealthPointChanged += RenderHealthPoint;
            Init();
        }
        
        private void Init()
        {
            var playerBeh = FindObjectOfType<PlayerBehavior>();
            healthPoint.maxValue = playerBeh._healthPoint;
        }

        private void OnDisable()
        {
            playerEventHandler.OnHealthPointChanged -= RenderHealthPoint;
        }

        private void RenderHealthPoint(int hp)
        {
            healthPoint.value = hp;
        }
    }
}