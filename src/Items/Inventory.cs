using System;
using System.Collections.Generic;

namespace src.Items
{
    public class Inventory
    {
        public int Capacity {  get; }
        public List<Consumable> Items { get; }

        public Inventory(int capacity)
        {
            Capacity = capacity;
            Items = new();
        }

        public bool AddItem(Consumable item)
        {
            if (Items.Count >= Capacity)
            {
                return false;
            }

            Items.Add(item);
            return true;
        }

        public void RemoveItem(int idx)
        {
            Items.RemoveAt(idx);
        }

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
