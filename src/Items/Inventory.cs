using System;
using System.Collections.Generic;

namespace src.Items
{
    public class Inventory
    {
        public int Capacity {  get; }
        public List<Item> Items { get; }

        public Inventory(int capacity)
        {
            Capacity = capacity;
            Items = new();
        }

        public bool AddItem(Item item)
        {
            if (Items.Count >= Capacity)
            {
                return false;
            }

            Items.Add(item);
            return true;
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }
    }

    public class EquipmentSlots
    {
        public Weapon? EquippedWeapon { get; private set; }

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
