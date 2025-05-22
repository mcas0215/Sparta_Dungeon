using System.Collections.Generic;
using UnityEngine;


public class PlayerInventory : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public ItemData data;
        public int quantity;

        public InventoryItem(ItemData data, int quantity = 1)
        {
            this.data = data;
            this.quantity = quantity;
        }
    }

    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(ItemData newItem)
    {
        if (newItem.canStack)
        {
            var existing = items.Find(i => i.data == newItem);
            if (existing != null)
            {
                if (existing.quantity < newItem.maxStackAmount)
                {
                    existing.quantity++;
                    return;
                }
            }
        }

        items.Add(new InventoryItem(newItem));
    }

    public void RemoveItem(ItemData item)
    {
        var existing = items.Find(i => i.data == item);
        if (existing != null)
        {
            existing.quantity--;
            if (existing.quantity <= 0)
            {
                items.Remove(existing);
            }
        }
    }
}

