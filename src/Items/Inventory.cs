using System;
using System.Collections.Generic;

namespace src.Items
{
    /// <summary>
    /// Represents a fixed-capacity inventory for storing consumable items
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// Gets the maximum number of items the inventory can hold
        /// </summary>
        public int Capacity {  get; }

        /// <summary>
        /// Gets the list of items currently stored in the inventory
        /// </summary>
        public List<Consumable> Items { get; }

        public Inventory(int capacity)
        {
            Capacity = capacity;
            Items = new();
        }

        /// <summary>
        /// Attempts to add an item to the inventory
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns>
        /// <c>true</c> if the item was added successfully; otherwise, <c>false</c> if the inventory is full
        /// </returns>
        public bool AddItem(Consumable item)
        {
            if (Items.Count >= Capacity)
            {
                return false;
            }

            Items.Add(item);
            return true;
        }

        /// <summary>
        /// Removes an item at the specified index from the inventory
        /// </summary>
        /// <param name="idx">The index of the item to remove</param>
        public void RemoveItem(int idx)
        {
            if (idx >= 0 && idx < Items.Count)
            {
                Items.RemoveAt(idx);
            }
        }

        /// <summary>
        /// Returns a formatted string listing all items in the inventory
        /// </summary>
        /// <returns>A numbered list of item names, or an empty string if inventory is empty</returns>
        public string GetItemList()
        {
            string output = "";
            for (int i = 0; i < Items.Count; i++)
            {
                output += $"{i + 1}. {Items[i].Name}\n";
            }
            return output;
        }
    }

    /// <summary>
    /// Represents equipment slots for the player, currently supporting a single weapon slot
    /// </summary>
    public class EquipmentSlots
    {
        /// <summary>
        /// Gets the currently equipped weapon, if any
        /// </summary>
        public Weapon? EquippedWeapon { get; private set; }

        /// <summary>
        /// Equips a weapon and returns the previously equipped weapon, if it existed
        /// </summary>
        /// <param name="weapon">The new weapon to equip, or <c>null</c> to unequip</param>
        /// <returns>The previously equipped weapon, or <c>null</c> if there was none</returns>
        public Weapon? EquipWeapon(Weapon? weapon)
        {
            Weapon? previous = null;
            if (EquippedWeapon != null)
            {
                previous = EquippedWeapon;
            }
            EquippedWeapon = weapon;
            return previous;
        }
    }
}
