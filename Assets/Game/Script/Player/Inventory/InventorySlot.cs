using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Script.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        private Image image;
        private Item item;
        public int index;
        public PlayerEventHandler playerEventHandler;

        private void Start()
        {
            image = GetComponent<Image>();
            playerEventHandler = FindObjectOfType<PlayerEventHandler>();
        }

        public void SetItem(Item item)
        {
            if (item == null)
            {
                item = null;
                image.sprite = null;
                image.color = new Color(0,0,0,0);
                return;
            }
            image.color = new Color(1,1,1,1);
            this.item = item;
            image.sprite = item.sprite;
        }

        public void RemoveItem()
        {
            playerEventHandler.OnRemoveItem(index);
            item = null;
            image.sprite = null;
        }
    }
}