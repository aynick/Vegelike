using System;
using System.Collections;
using System.Collections.Generic;
using Game.Script;
using Game.Script.Inventory;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Item[] items = new Item[6];
    private int maxItemsCount = 6;
    private PlayerCanvas playerCanvas;
    private PlayerEventHandler playerEventHandler;
    [SerializeField] private Item item;
    
    private void Start()
    {
        playerEventHandler = GetComponent<PlayerEventHandler>();
        playerEventHandler.OnRemovedItem += Remove;
        playerCanvas = FindObjectOfType<PlayerCanvas>();
        RenderSlots();
    }

    void RenderSlots()
    {
        playerEventHandler.OnRenderSlots(items);
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                RenderSlots();
                return;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddItem(item);
        }
    }

    public void Remove(int index)
    {
        items[index] = null;
        RenderSlots();
    }
}
