using System;
using System.Collections.Generic;
using Game.Script.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Script
{
    public class CanvasRender : MonoBehaviour
    {
        private PlayerEventHandler playerEventHandler;
        private PlayerCanvas playerCanvas;
        [SerializeField] private Slider healthPoint;
        private List<InventorySlot> inventorySlots;

        private void Start() 
        { 
            
            Init();
        }
        
        private void Init()
        {
            var playerBeh = FindObjectOfType<PlayerBehavior>();
            playerCanvas = FindObjectOfType<PlayerCanvas>();
            playerEventHandler = FindObjectOfType<PlayerEventHandler>();
            playerEventHandler.OnHealthPointChanged += RenderHealthPoint;
            playerEventHandler.OnRenderedSlots += RenderSlots;
            inventorySlots = playerCanvas.slots;
        }

        private void OnDisable()
        {
            playerEventHandler.OnHealthPointChanged -= RenderHealthPoint;
        }

        private void RenderSlots(Item[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                inventorySlots[i].SetItem(items[i]);
            }   
        }

        private void RenderHealthPoint(int hp,int maxHp)
        {
            healthPoint.maxValue = maxHp;
            healthPoint.value = hp;
        }
    }
}